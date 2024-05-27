using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Application.Exceptions;
using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Infrastructure.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqDomainEventPublisher : IDomainEventPublisher
{
    private readonly RabbitmqMessagePublisher messagePublisher;
    private readonly ApplicationExceptionHandler exceptionHandler;

    public RabbitmqDomainEventPublisher(
        RabbitmqMessagePublisher messagePublisher,
        ApplicationExceptionHandler exceptionHandler
    )
    {
        this.messagePublisher = messagePublisher;
        this.exceptionHandler = exceptionHandler;
    }

    public async void Publish(List<DomainEvent> events)
    {
        foreach (DomainEvent _event in events)
        {
            await Task.Run(() => PublishEvent(_event));
        }
    }

    // REFACTOR: Change the name of this method to 'Publish'
    private void PublishEvent(DomainEvent _event)
    {
        byte[] messageBody = JsonDomainEventSerializer.Serialize(_event);
        string eventName = _event.EventName;
        try
        {
            messagePublisher.Publish(eventName, messageBody);
        }
        catch (ApplicationException exception)
        {
            exceptionHandler.Handle(exception);
        }
    }
}
