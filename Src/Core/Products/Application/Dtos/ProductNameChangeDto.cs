namespace Src.Core.Products.Application.Dtos;

public record ProductNameChangeDto
{
    public string Id { get; }
    public string Name { get; }
    public string RestaurantId { get; }

    public ProductNameChangeDto(string id, string name, string restaurantId)
    {
        Id = id;
        Name = name;
        RestaurantId = restaurantId;
    }
}
