using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Domain.Aggregates;

public class AggregateRoot
{
    private readonly List<DomainEvent> events;

    public AggregateRoot()
    {
        events = new List<DomainEvent>();
    }

    public void RecordEvent(DomainEvent _event)
    {
        events.Add(_event);
    }

    public List<DomainEvent> PullEvents()
    {
        return events;
    }
}
