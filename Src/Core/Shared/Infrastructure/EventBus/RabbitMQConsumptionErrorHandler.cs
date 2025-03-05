using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Src.Core.Shared.Application.Events;
using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Generators;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitMQConsumptionErrorHandler
{
    private const string DeliveryAttemptsHeader = "delivery_attempts";
    private const string QueueHeader = "queue";
    private const string TimestampHeader = "timestamp";
    private const string DeliveryDelayHeader = "x-delay";

    private readonly RabbitMQMessagePublisher messagePublisher;
    private readonly CustomExceptionHandler exceptionHandler;
    private readonly int messageDeliveryMode;
    private readonly int messageDeliveryLimit;
    private readonly int messageRedeliveryDelay;

    public RabbitMQConsumptionErrorHandler(
        RabbitMQMessagePublisher messagePublisher,
        CustomExceptionHandler exceptionHandler
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
            Environment.GetEnvironmentVariable(
                "EVENT_BUS_MESSAGE_REDELIVERY_DELAY_IN_MILLISECONDS"
            )!
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
        catch (CustomException exception)
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
        if (messageHeaders?.ContainsKey(DeliveryAttemptsHeader) == true)
        {
            return (int)messageHeaders[DeliveryAttemptsHeader];
        }
        return 0;
    }

    private void SendToDeadLetter(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        string queueName = RabbitMQQueueNameFormatter.Format(eventInformation);
        IBasicProperties properties = deliverEventArgs.BasicProperties;
        properties.DeliveryMode = (byte)messageDeliveryMode;
        properties.Headers = new Dictionary<string, object>()
        {
            { QueueHeader, queueName },
            { TimestampHeader, TimestampGenerator.Generate() },
        };
        byte[] messageBody = deliverEventArgs.Body.ToArray();
        string exchangeName = RabbitMQExchangeNameFormatter.FormatToDeadLetter();
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
            { DeliveryDelayHeader, messageRedeliveryDelay },
            { DeliveryAttemptsHeader, deliveryAttempts },
        };
        byte[] messageBody = deliverEventArgs.Body.ToArray();
        string exchangeName = RabbitMQExchangeNameFormatter.FormatToRetry(eventInformation);
        messagePublisher.Publish(exchangeName, messageBody, properties);
    }
}
