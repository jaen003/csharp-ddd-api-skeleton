using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Infrastructure.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public class RabbitmqDomainEventPublisher : IDomainEventPublisher
{
    private readonly RabbitmqMessagePublisher messagePublisher;
    private readonly DomainExceptionHandler exceptionHandler;

    public RabbitmqDomainEventPublisher(
        RabbitmqMessagePublisher messagePublisher,
        DomainExceptionHandler exceptionHandler
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

    private void PublishEvent(DomainEvent _event)
    {
        byte[] messageBody = JsonDomainEventSerializer.Serialize(_event);
        string eventName = _event.EventName;
        try
        {
            messagePublisher.Publish(eventName, messageBody);
        }
        catch (DomainException exception)
        {
            exceptionHandler.Handle(exception);
        }
    }
}
