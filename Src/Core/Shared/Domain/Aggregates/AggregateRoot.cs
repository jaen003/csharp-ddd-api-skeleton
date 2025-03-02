using System.Collections.ObjectModel;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Domain.Aggregates;

public class AggregateRoot
{
    private readonly List<DomainEvent> events;

    public AggregateRoot()
    {
        events = [];
    }

    protected void RecordEvent(DomainEvent domainEvent)
    {
        events.Add(domainEvent);
    }

    public ReadOnlyCollection<DomainEvent> PullEvents()
    {
        return events.AsReadOnly();
    }
}
