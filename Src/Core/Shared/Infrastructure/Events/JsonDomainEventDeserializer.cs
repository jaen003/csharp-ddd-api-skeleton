using System.Text.Json;
using Src.Core.Shared.Domain.Events;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Events;

public static class JsonDomainEventDeserializer
{
    public static DomainEvent Deserialize(byte[] messageBody, Type eventClass)
    {
        DomainEvent domainEvent = (DomainEvent)Activator.CreateInstance(eventClass)!;
        try
        {
            Dictionary<string, object> messageData = JsonSerializer.Deserialize<
                Dictionary<string, object>
            >(messageBody)!;
            return domainEvent.FromPrimitives(
                messageData["id"].ToString()!,
                int.Parse(messageData["timestamp"].ToString()!),
                JsonSerializer.Deserialize<Dictionary<string, object>>(
                    messageData["data"].ToString()!
                )!
            );
        }
        catch (Exception)
        {
            throw new InvalidDomainEventStructure(domainEvent.EventName);
        }
    }
}
