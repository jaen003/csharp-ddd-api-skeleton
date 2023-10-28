using Src.Core.Shared.Domain.Events;

namespace Src.Core.Restaurants.Domain.Events;

public class RestaurantCreated : DomainEvent
{
    public string Id { get; }
    public string Name { get; }
    public override string EventName => "restaurant.created";

    public RestaurantCreated(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public RestaurantCreated(string id, string name, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
    }

    public RestaurantCreated()
    {
        Name = string.Empty;
        Id = string.Empty;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new RestaurantCreated(
            data["id"].ToString()!,
            data["name"].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id }, { "name", Name } };
    }
}
