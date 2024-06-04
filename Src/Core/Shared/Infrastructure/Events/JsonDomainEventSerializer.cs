using System.Text;
using System.Text.Json;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.Events;

public static class JsonDomainEventSerializer
{
    public static byte[] Serialize(DomainEvent _event)
    {
        Dictionary<string, object> messageData =
            new()
            {
                { "id", _event.EventId },
                { "name", _event.EventName },
                { "timestamp", _event.Timestamp },
                { "data", _event.ToPrimitives() }
            };
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageData));
    }
}
