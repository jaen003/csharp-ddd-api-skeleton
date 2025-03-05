using System.Collections.ObjectModel;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Infrastructure.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitMQDomainEventPublisher : IDomainEventPublisher
{
    private readonly RabbitMQMessagePublisher messagePublisher;
    private readonly CustomExceptionHandler exceptionHandler;

    public RabbitMQDomainEventPublisher(
        RabbitMQMessagePublisher messagePublisher,
        CustomExceptionHandler exceptionHandler
    )
    {
        this.messagePublisher = messagePublisher;
        this.exceptionHandler = exceptionHandler;
    }

    public async Task Publish(ReadOnlyCollection<DomainEvent> events)
    {
        Task[] publishingTasks = events.Select(Publish).ToArray();
        await Task.WhenAll(publishingTasks);
    }

    private async Task Publish(DomainEvent domainEvent)
    {
        byte[] messageBody = JsonDomainEventSerializer.Serialize(domainEvent);
        string eventName = domainEvent.EventName;
        try
        {
            await Task.Run(() => messagePublisher.Publish(eventName, messageBody));
        }
        catch (CustomException exception)
        {
            exceptionHandler.Handle(exception);
        }
    }
}
