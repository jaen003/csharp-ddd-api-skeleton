using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Domain.Aggregates;

public class AggregateRoot
{
    private readonly List<DomainEvent> events;

    public AggregateRoot()
    {
        events = new List<DomainEvent>();
    }

    // REFACTOR: Change method access modifier to 'protected'
    public void RecordEvent(DomainEvent _event)
    {
        events.Add(_event);
    }

    // REFACTOR: Change the return type to 'IReadOnlyList<DomainEvent>'
    public List<DomainEvent> PullEvents()
    {
        return events;
    }
}
