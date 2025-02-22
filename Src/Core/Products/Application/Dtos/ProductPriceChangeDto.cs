namespace Src.Core.Products.Application.Dtos;

public record ProductPriceChangeDto
{
    public Guid Id { get; }
    public int Price { get; }
    public Guid RestaurantId { get; }

    public ProductPriceChangeDto(Guid id, int price, Guid restaurantId)
    {
        Id = id;
        Price = price;
        RestaurantId = restaurantId;
    }
}
