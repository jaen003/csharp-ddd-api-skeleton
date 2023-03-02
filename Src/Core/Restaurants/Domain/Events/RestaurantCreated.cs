using Src.Core.Shared.Domain.Events;

namespace Src.Core.Restaurants.Domain.Events;

public class RestaurantCreated : DomainEvent
{
    public long Id { get; }
    public string Name { get; }
    public override string EventName => "restaurant.created";

    public RestaurantCreated(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public RestaurantCreated(long id, string name, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
    }

    public RestaurantCreated()
    {
        Name = string.Empty;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new RestaurantCreated(
            long.Parse(data["id"].ToString()!),
            data["name"].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id }, { "name", "" + Name } };
    }
}
