using RabbitMQ.Client;
using Src.Core.Shared.Application.Events;
using Src.Core.Shared.Infrastructure.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitMQEventBusConfigurer
{
    private const string ExchangeType = "fanout";
    private const string DelayedExchangeType = "x-delayed-message";
    private const string DelayedExchangeTypeHeader = "x-delayed-type";

    private readonly RabbitMQEventBusConnection eventBusConnection;
    private readonly DomainEventInformationCollection eventInformationCollection;

    public RabbitMQEventBusConfigurer(
        RabbitMQEventBusConnection eventBusConnection,
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
            throw new EventBusNotConfiguredException(exception.ToString());
        }
    }

    private async Task CreateDeadLetterQueue(IModel channel)
    {
        if (!eventInformationCollection.IsEmpty())
        {
            string deadLetterQueueName = RabbitMQQueueNameFormatter.FormatToDeadLetter();
            string deadLetterExchangeName = RabbitMQExchangeNameFormatter.FormatToDeadLetter();
            await DeclareQueue(deadLetterQueueName, channel);
            await DeclareExchange(deadLetterExchangeName, channel);
            await BindQueue(deadLetterQueueName, deadLetterExchangeName, channel);
        }
    }

    private async Task CreateDomainEvents(IModel channel)
    {
        foreach (DomainEventInformation eventInformation in eventInformationCollection.GetAll())
        {
            string exchangeName = RabbitMQExchangeNameFormatter.Format(eventInformation);
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
        string exchangeName = RabbitMQExchangeNameFormatter.Format(eventInformation);
        string queueName = RabbitMQQueueNameFormatter.Format(eventInformation);
        await DeclareQueue(queueName, channel);
        await BindQueue(queueName, exchangeName, channel);
        string retryExchangeName = RabbitMQExchangeNameFormatter.FormatToRetry(eventInformation);
        await DeclareDelayedExchange(retryExchangeName, channel);
        await BindQueue(queueName, retryExchangeName, channel);
    }

    private static async Task DeclareExchange(string exchangeName, IModel channel)
    {
        await Task.Run(() => channel.ExchangeDeclare(exchangeName, ExchangeType, true));
    }

    private static async Task DeclareDelayedExchange(string exchangeName, IModel channel)
    {
        Dictionary<string, object> arguments =
            new() { { DelayedExchangeTypeHeader, ExchangeType } };
        await Task.Run(
            () => channel.ExchangeDeclare(exchangeName, DelayedExchangeType, true, false, arguments)
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
