namespace Src.Core.Restaurants.Application.Dtos;

public record RestaurantCreationDto
{
    public Guid Id { get; }
    public string Name { get; }

    public RestaurantCreationDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
