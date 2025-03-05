namespace Src.Core.Products.Application.Dtos;

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
