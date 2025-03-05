namespace Src.Core.Products.Application.DTOs;

public record ProductSearchData
{
    public Guid Id { get; }
    public Guid RestaurantId { get; }

    public ProductSearchData(Guid id, Guid restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
