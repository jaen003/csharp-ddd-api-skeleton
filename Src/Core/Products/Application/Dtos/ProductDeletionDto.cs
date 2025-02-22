namespace Src.Core.Products.Application.Dtos;

public record ProductDeletionDto
{
    public Guid Id { get; }
    public Guid RestaurantId { get; }

    public ProductDeletionDto(Guid id, Guid restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
