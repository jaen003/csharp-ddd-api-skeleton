using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.EventHandlers;

public interface IDomainEventHandler<T> : IDomainEventHandlerBase
    where T : DomainEvent
{
    Task Handle(T domainEvent);

    async Task IDomainEventHandlerBase.Handle(DomainEvent domainEvent)
    {
        if (domainEvent is T genericEvent)
        {
            await Handle(genericEvent);
        }
    }
}
