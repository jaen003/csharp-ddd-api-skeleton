namespace Src.Core.Products.Application.Dtos;

public record ProductCreationDto
{
    public string Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }
    public string RestaurantId { get; }

    public ProductCreationDto(
        string id,
        string name,
        int price,
        string description,
        string restaurantId
    )
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        RestaurantId = restaurantId;
    }
}
