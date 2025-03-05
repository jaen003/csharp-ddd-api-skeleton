namespace Src.Core.Products.Application.Dtos;

public record ProductPriceChangeData
{
    public Guid Id { get; }
    public int Price { get; }
    public Guid RestaurantId { get; }

    public ProductPriceChangeData(Guid id, int price, Guid restaurantId)
    {
        Id = id;
        Price = price;
        RestaurantId = restaurantId;
    }
}
