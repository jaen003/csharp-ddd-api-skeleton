using RabbitMQ.Client;
using Src.Core.Shared.Application.Events;
using Src.Core.Shared.Infrastructure.Exceptions;

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
            using IModel channel = eventBusConnection.GetChannel()!;
            await CreateDeadLetterQueue(channel);
            await CreateDomainEvents(channel);
        }
        catch (Exception exception)
        {
            throw new EventBusNotConfigured(exception.ToString());
        }
    }

    private async Task CreateDeadLetterQueue(IModel channel)
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
            await DeclareQueue(deadLetterQueueName, channel);
            await DeclareExchange(deadLetterExchangeName, channel);
            await BindQueue(deadLetterQueueName, deadLetterExchangeName, channel);
        }
    }

    private async Task CreateDomainEvents(IModel channel)
    {
        foreach (DomainEventInformation eventInformation in eventInformationCollection.GetAll())
        {
            string exchangeName = RabbitmqExchangeNameFormatter.Format(eventInformation);
            await DeclareExchange(exchangeName, channel);
            if (eventInformation.HasEventHandlers())
            {
                await CreateDomainEventQueues(eventInformation, channel);
            }
        }
    }

    private static async Task CreateDomainEventQueues(
        DomainEventInformation eventInformation,
        IModel channel
    )
    {
        string exchangeName = RabbitmqExchangeNameFormatter.Format(eventInformation);
        string queueName = RabbitmqQueueNameFormatter.Format(eventInformation);
        await DeclareQueue(queueName, channel);
        await BindQueue(queueName, exchangeName, channel);
        string retryExchangeName = RabbitmqExchangeNameFormatter.FormatToRetry(eventInformation);
        await DeclareDelayedExchange(retryExchangeName, channel);
        await BindQueue(queueName, retryExchangeName, channel);
    }

    private static async Task DeclareExchange(string exchangeName, IModel channel)
    {
        await Task.Run(() => channel.ExchangeDeclare(exchangeName, EXCHANGE_TYPE, true));
    }

    private static async Task DeclareDelayedExchange(string exchangeName, IModel channel)
    {
        Dictionary<string, object> arguments =
            new() { { DELAYED_EXCHANGE_TYPE_HEADER, EXCHANGE_TYPE } };
        await Task.Run(
            () =>
                channel.ExchangeDeclare(exchangeName, DELAYED_EXCHANGE_TYPE, true, false, arguments)
        );
    }

    private static async Task BindQueue(string queueName, string exchangeName, IModel channel)
    {
        await Task.Run(() => channel.QueueBind(queueName, exchangeName, ""));
    }

    private static async Task DeclareQueue(string queueName, IModel channel)
    {
        await Task.Run(() => channel.QueueDeclare(queueName, true, false, false));
    }
}
