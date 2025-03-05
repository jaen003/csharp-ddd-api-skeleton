using Src.Core.Shared.Domain.Events;

namespace Src.Core.Restaurants.Domain.Events;

public class RestaurantCreatedDomainEvent : DomainEvent
{
    private const string IdField = "id";
    private const string NameField = "name";

    public Guid Id { get; }
    public string Name { get; }

    public override string EventName => "restaurant.created";

    private RestaurantCreatedDomainEvent(Guid id, string name, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
    }

    public RestaurantCreatedDomainEvent()
    {
        Name = string.Empty;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new RestaurantCreatedDomainEvent(
            new Guid(data[IdField].ToString()!),
            data[NameField].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { IdField, Id }, { NameField, Name } };
    }
}
