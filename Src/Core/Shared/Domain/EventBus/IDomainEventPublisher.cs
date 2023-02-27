using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Domain.EventBus;

public interface IDomainEventPublisher
{
    void Publish(List<DomainEvent> events);
}
