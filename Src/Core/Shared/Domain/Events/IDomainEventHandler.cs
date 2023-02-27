namespace Src.Core.Shared.Domain.Events;

public interface IDomainEventHandler<T> : IDomainEventHandlerBase
    where T : DomainEvent
{
    Task Handle(T _event);

    async Task IDomainEventHandlerBase.Handle(DomainEvent _event)
    {
        if (_event is T genericEvent)
        {
            await Handle(genericEvent);
        }
    }
}
