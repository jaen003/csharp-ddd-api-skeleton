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
                { "timestamp", _event.Timestamp }
            };
        Dictionary<string, object> data = _event.ToPrimitives();
        messageData.Add("data", data);
        return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageData));
    }
}
