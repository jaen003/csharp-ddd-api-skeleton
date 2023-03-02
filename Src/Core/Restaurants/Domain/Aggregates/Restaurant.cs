using Src.Core.Restaurants.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.Aggregates;

public class Restaurant
{
    public RestaurantId Id { get; }
    public RestaurantName Name { get; }
    public RestaurantStatus Status { get; }

    public Restaurant(RestaurantId id, RestaurantName name, RestaurantStatus status)
    {
        Id = id;
        Name = name;
        Status = status;
    }

    public static Restaurant Create(RestaurantId id, RestaurantName name)
    {
        return new(id, name, RestaurantStatus.CreateActived());
    }
}
