using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Infrastructure.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqDomainEventConsumer
{
    private readonly RabbitmqEventBusConnection eventBusConnection;
    private readonly DomainEventInformationCollection eventInformationCollection;
    private readonly DomainExceptionHandler exceptionHandler;
    private readonly RabbitmqConsumptionErrorHandler consumptionErrorHandler;
    private readonly IServiceProvider serviceProvider;

    public RabbitmqDomainEventConsumer(
        RabbitmqEventBusConnection eventBusConnection,
        DomainEventInformationCollection eventInformationCollection,
        DomainExceptionHandler exceptionHandler,
        RabbitmqConsumptionErrorHandler consumptionErrorHandler,
        IServiceProvider serviceProvider
    )
    {
        this.eventBusConnection = eventBusConnection;
        this.eventInformationCollection = eventInformationCollection;
        this.exceptionHandler = exceptionHandler;
        this.consumptionErrorHandler = consumptionErrorHandler;
        this.serviceProvider = serviceProvider;
    }

    public void Consume()
    {
        foreach (DomainEventInformation eventInformation in eventInformationCollection.GetAll())
        {
            if (eventInformation.HasEventHandlers())
            {
                ConsumeEvent(eventInformation);
            }
        }
    }

    private void ConsumeEvent(DomainEventInformation eventInformation)
    {
        try
        {
            IModel channel = eventBusConnection.GetChannel()!;
            AsyncEventingBasicConsumer consumer = new(channel);
            consumer.Received += (_, deliverEventArgs) =>
                Callback(deliverEventArgs, eventInformation);
            string queueName = RabbitmqQueueNameFormatter.Format(eventInformation);
            channel.BasicConsume(queueName, true, consumer);
        }
        catch (Exception exception)
        {
            throw new EventBusErrorException(exception.ToString());
        }
    }

    private async Task Callback(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        try
        {
            DomainEvent _event = JsonDomainEventDeserializer.Deserialize(
                deliverEventArgs.Body.ToArray(),
                eventInformation.EventClass
            );
            foreach (Type handlerClass in eventInformation.EventHandlerClasses)
            {
                IDomainEventHandlerBase eventHandler = (IDomainEventHandlerBase)
                    serviceProvider.GetRequiredService(handlerClass);
                await eventHandler.Handle(_event);
            }
        }
        catch (DomainException exception)
        {
            exceptionHandler.Handle(exception);
            consumptionErrorHandler.Handle(deliverEventArgs, eventInformation);
        }
    }
}
