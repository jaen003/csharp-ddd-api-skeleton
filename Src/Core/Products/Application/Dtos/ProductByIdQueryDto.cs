namespace Src.Core.Products.Application.Dtos;

public record ProductByIdQueryDto
{
    public string Id { get; }
    public string RestaurantId { get; }

    public ProductByIdQueryDto(string id, string restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
