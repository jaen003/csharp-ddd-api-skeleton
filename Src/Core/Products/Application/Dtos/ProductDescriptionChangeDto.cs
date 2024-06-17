namespace Src.Core.Products.Application.Dtos;

public record ProductDescriptionChangeDto
{
    public string Id { get; }
    public string Description { get; }
    public string RestaurantId { get; }

    public ProductDescriptionChangeDto(string id, string description, string restaurantId)
    {
        Id = id;
        Description = description;
        RestaurantId = restaurantId;
    }
}
