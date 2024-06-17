namespace Src.Core.Products.Application.Dtos;

public record ProductDeletionDto
{
    public string Id { get; }
    public string RestaurantId { get; }

    public ProductDeletionDto(string id, string restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
