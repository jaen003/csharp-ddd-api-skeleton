namespace Src.Core.Products.Application.Dtos;

public record ProductDeletionData
{
    public Guid Id { get; }
    public Guid RestaurantId { get; }

    public ProductDeletionData(Guid id, Guid restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
