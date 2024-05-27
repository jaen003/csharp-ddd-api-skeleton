using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.EventHandlers;

public interface IDomainEventHandlerBase
{
    Task Handle(DomainEvent _event);
}
