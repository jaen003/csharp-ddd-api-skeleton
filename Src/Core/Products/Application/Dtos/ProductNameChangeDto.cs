namespace Src.Core.Products.Application.Dtos;

public record ProductNameChangeDto
{
    public Guid Id { get; }
    public string Name { get; }
    public Guid RestaurantId { get; }

    public ProductNameChangeDto(Guid id, string name, Guid restaurantId)
    {
        Id = id;
        Name = name;
        RestaurantId = restaurantId;
    }
}
