using System.Collections.ObjectModel;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.EventBus;

public interface IDomainEventPublisher
{
    Task Publish(ReadOnlyCollection<DomainEvent> events);
}
