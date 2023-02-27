using RabbitMQ.Client;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqEventBusConfigurer
{
    private const string EXCHANGE_TYPE = "fanout";
    private const string DELAYED_EXCHANGE_TYPE = "x-delayed-message";
    private const string DELAYED_EXCHANGE_TYPE_HEADER = "x-delayed-type";

    private readonly RabbitmqEventBusConnection eventBusConnection;
    private readonly DomainEventInformationCollection eventInformationCollection;

    public RabbitmqEventBusConfigurer(
        RabbitmqEventBusConnection eventBusConnection,
        DomainEventInformationCollection eventInformationCollection
    )
    {
        this.eventBusConnection = eventBusConnection;
        this.eventInformationCollection = eventInformationCollection;
    }

    public async Task Configure()
    {
        try
        {
            await Task.Run(() => CreateDeadLetterQueue());
            await Task.Run(() => CreateEvents());
        }
        catch (Exception exception)
        {
            throw new EventBusErrorException(exception.ToString());
        }
    }

    private void CreateDeadLetterQueue()
    {
        DomainEventInformation? eventInformation = eventInformationCollection.GetFirst();
        if (eventInformation != null)
        {
            string deadLetterQueueName = RabbitmqQueueNameFormatter.FormatToDeadLetter(
                eventInformation
            );
            string deadLetterExchangeName = RabbitmqExchangeNameFormatter.FormatToDeadLetter(
                eventInformation
            );
            DeclareQueue(deadLetterQueueName);
            DeclareExchange(deadLetterExchangeName);
            BindQueue(deadLetterQueueName, deadLetterExchangeName);
        }
    }

    private void CreateEvents()
    {
        foreach (DomainEventInformation eventInformation in eventInformationCollection.GetAll())
        {
            string exchangeName = RabbitmqExchangeNameFormatter.Format(eventInformation);
            DeclareExchange(exchangeName);
            if (eventInformation.HasEventHandlers())
            {
                CreateEventQueues(eventInformation);
            }
        }
    }

    private void CreateEventQueues(DomainEventInformation eventInformation)
    {
        string exchangeName = RabbitmqExchangeNameFormatter.Format(eventInformation);
        string queueName = RabbitmqQueueNameFormatter.Format(eventInformation);
        DeclareQueue(queueName);
        BindQueue(queueName, exchangeName);
        string retryExchangeName = RabbitmqExchangeNameFormatter.FormatToRetry(eventInformation);
        DeclareDelayedExchange(retryExchangeName);
        BindQueue(queueName, retryExchangeName);
    }

    private void DeclareExchange(string exchangeName)
    {
        using IModel channel = eventBusConnection.GetChannel()!;
        channel.ExchangeDeclare(exchangeName, EXCHANGE_TYPE, true);
    }

    private void DeclareDelayedExchange(string exchangeName)
    {
        using IModel channel = eventBusConnection.GetChannel()!;
        Dictionary<string, object> arguments =
            new() { { DELAYED_EXCHANGE_TYPE_HEADER, EXCHANGE_TYPE } };
        channel.ExchangeDeclare(exchangeName, DELAYED_EXCHANGE_TYPE, true, false, arguments);
    }

    private void BindQueue(string queueName, string exchangeName)
    {
        using IModel channel = eventBusConnection.GetChannel()!;
        channel.QueueBind(queueName, exchangeName, "");
    }

    private void DeclareQueue(string queueName)
    {
        using IModel channel = eventBusConnection.GetChannel()!;
        channel.QueueDeclare(queueName, true, false, false);
    }
}
