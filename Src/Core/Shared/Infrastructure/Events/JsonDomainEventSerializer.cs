using System.Text;
using System.Text.Json;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.Events;

public static class JsonDomainEventSerializer
{
    public static byte[] Serialize(DomainEvent domainEvent)
    {
        Dictionary<string, object> messageData =
            new()
            {
                { "id", domainEvent.EventId },
                { "name", domainEvent.EventName },
                { "timestamp", domainEvent.Timestamp },
                { "data", domainEvent.ToPrimitives() }
            };
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageData));
    }
}
