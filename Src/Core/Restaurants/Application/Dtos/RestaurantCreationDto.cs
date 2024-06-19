namespace Src.Core.Restaurants.Application.Dtos;

public record RestaurantCreationDto
{
    public string Id { get; }
    public string Name { get; }

    public RestaurantCreationDto(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
