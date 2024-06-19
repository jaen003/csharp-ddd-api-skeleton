using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Domain.Aggregates;

public class AggregateRoot
{
    private readonly List<DomainEvent> events;

    public AggregateRoot()
    {
        events = new List<DomainEvent>();
    }

    protected void RecordEvent(DomainEvent domainEvent)
    {
        events.Add(domainEvent);
    }

    public List<DomainEvent> PullEvents()
    {
        return events;
    }
}
