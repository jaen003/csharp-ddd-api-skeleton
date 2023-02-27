namespace Src.Core.Shared.Domain.Events;

public interface IDomainEventHandlerBase
{
    Task Handle(DomainEvent _event);
}
