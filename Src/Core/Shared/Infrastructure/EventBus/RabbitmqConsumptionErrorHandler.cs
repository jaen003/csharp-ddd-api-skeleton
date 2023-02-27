using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Geneators;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqConsumptionErrorHandler
{
    private const string DELIVERY_ATTEMPTS_HEADER = "delivery_attempts";
    private const string QUEUE_HEADER = "queue";
    private const string TIMESTAMP_HEADER = "timestamp";
    private const string DELIVERY_DELAY_HEADER = "x-delay";

    private readonly RabbitmqMessagePublisher messagePublisher;
    private readonly DomainExceptionHandler exceptionHandler;
    private readonly int messageDeliveryMode;
    private readonly int messageDeliveryLimit;
    private readonly int messageRedeliveryDelay;

    public RabbitmqConsumptionErrorHandler(
        RabbitmqMessagePublisher messagePublisher,
        DomainExceptionHandler exceptionHandler
    )
    {
        this.messagePublisher = messagePublisher;
        this.exceptionHandler = exceptionHandler;
        messageDeliveryMode = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_MESSAGE_DELIVERY_MODE")!
        );
        messageDeliveryLimit = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_MESSAGE_DELIVERY_LIMIT")!
        );
        messageRedeliveryDelay = int.Parse(
            Environment.GetEnvironmentVariable("EVENT_BUS_MESSAGE_REDELIVERY_DELAY")!
        );
    }

    public void Handle(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        try
        {
            if (HasBeenRetriedTooMuch(deliverEventArgs))
            {
                SendToDeadLetter(deliverEventArgs, eventInformation);
            }
            else
            {
                SendToRetry(deliverEventArgs, eventInformation);
            }
        }
        catch (DomainException exception)
        {
            exceptionHandler.Handle(exception);
        }
    }

    private bool HasBeenRetriedTooMuch(BasicDeliverEventArgs deliverEventArgs)
    {
        int deliveryAttempts = GetDeliveryAttempts(deliverEventArgs);
        return deliveryAttempts >= (messageDeliveryLimit - 1);
    }

    private static int GetDeliveryAttempts(BasicDeliverEventArgs deliverEventArgs)
    {
        IDictionary<string, object>? messageHeaders = deliverEventArgs.BasicProperties.Headers;
        if (messageHeaders?.ContainsKey(DELIVERY_ATTEMPTS_HEADER) == true)
        {
            return (int)messageHeaders[DELIVERY_ATTEMPTS_HEADER];
        }
        return 0;
    }

    private void SendToDeadLetter(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        string queueName = RabbitmqQueueNameFormatter.Format(eventInformation);
        IBasicProperties properties = deliverEventArgs.BasicProperties;
        properties.DeliveryMode = (byte)messageDeliveryMode;
        properties.Headers = new Dictionary<string, object>()
        {
            { QUEUE_HEADER, queueName },
            { TIMESTAMP_HEADER, TimestampGenerator.Generate() }
        };
        byte[] messageBody = deliverEventArgs.Body.ToArray();
        string exchangeName = RabbitmqExchangeNameFormatter.FormatToDeadLetter(eventInformation);
        messagePublisher.Publish(exchangeName, messageBody, properties);
    }

    private void SendToRetry(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        int deliveryAttempts = GetDeliveryAttempts(deliverEventArgs);
        deliveryAttempts++;
        IBasicProperties properties = deliverEventArgs.BasicProperties;
        properties.DeliveryMode = (byte)messageDeliveryMode;
        properties.Headers = new Dictionary<string, object>()
        {
            { DELIVERY_DELAY_HEADER, messageRedeliveryDelay },
            { DELIVERY_ATTEMPTS_HEADER, deliveryAttempts }
        };
        byte[] messageBody = deliverEventArgs.Body.ToArray();
        string exchangeName = RabbitmqExchangeNameFormatter.FormatToRetry(eventInformation);
        messagePublisher.Publish(exchangeName, messageBody, properties);
    }
}
