using Src.Core.Shared.Domain.Generators;

namespace Src.Core.Shared.Domain.Events;

public abstract class DomainEvent
{
    public string EventId { get; }
    public int Timestamp { get; }
    public abstract string EventName { get; }

    protected DomainEvent(string eventId, int timestamp)
    {
        EventId = eventId;
        Timestamp = timestamp;
    }

    protected DomainEvent()
    {
        Timestamp = TimestampGenerator.Generate();
        EventId = Guid.NewGuid().ToString();
    }

    public abstract Dictionary<string, object> ToPrimitives();

    public abstract DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    );
}
