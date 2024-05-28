using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.EventBus;

public interface IDomainEventPublisher
{
    Task Publish(List<DomainEvent> events);
}
