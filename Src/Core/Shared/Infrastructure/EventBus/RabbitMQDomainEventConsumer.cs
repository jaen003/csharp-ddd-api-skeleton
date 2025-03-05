using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Src.Core.Shared.Application.EventHandlers;
using Src.Core.Shared.Application.Events;
using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Infrastructure.Events;
using Src.Core.Shared.Infrastructure.Exceptions;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitMQDomainEventConsumer
{
    private readonly RabbitMQEventBusConnection eventBusConnection;
    private readonly DomainEventInformationCollection eventInformationCollection;
    private readonly CustomExceptionHandler exceptionHandler;
    private readonly RabbitMQConsumptionErrorHandler consumptionErrorHandler;
    private readonly IServiceProvider serviceProvider;

    public RabbitMQDomainEventConsumer(
        RabbitMQEventBusConnection eventBusConnection,
        DomainEventInformationCollection eventInformationCollection,
        CustomExceptionHandler exceptionHandler,
        RabbitMQConsumptionErrorHandler consumptionErrorHandler,
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
            string queueName = RabbitMQQueueNameFormatter.Format(eventInformation);
            channel.BasicConsume(queueName, true, consumer);
        }
        catch (Exception exception)
        {
            throw new DomainEventConsumptionFailedException(exception.ToString());
        }
    }

    private async Task Callback(
        BasicDeliverEventArgs deliverEventArgs,
        DomainEventInformation eventInformation
    )
    {
        try
        {
            DomainEvent domainEvent = JsonDomainEventDeserializer.Deserialize(
                deliverEventArgs.Body.ToArray(),
                eventInformation.EventClass
            );
            foreach (Type handlerClass in eventInformation.EventHandlerClasses)
            {
                IDomainEventHandlerBase eventHandler = (IDomainEventHandlerBase)
                    serviceProvider.GetRequiredService(handlerClass);
                await eventHandler.Handle(domainEvent);
            }
        }
        catch (CustomException exception)
        {
            exceptionHandler.Handle(exception);
            consumptionErrorHandler.Handle(deliverEventArgs, eventInformation);
        }
        catch (MultipleCustomException multipleException)
        {
            foreach (CustomException exception in multipleException.Exceptions)
            {
                exceptionHandler.Handle(exception);
            }
            consumptionErrorHandler.Handle(deliverEventArgs, eventInformation);
        }
    }
}
