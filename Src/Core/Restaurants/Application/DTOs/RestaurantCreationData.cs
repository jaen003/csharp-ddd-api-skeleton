namespace Src.Core.Restaurants.Application.DTOs;

public record RestaurantCreationData
{
    public Guid Id { get; }
    public string Name { get; }

    public RestaurantCreationData(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
