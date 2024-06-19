namespace Src.Core.Products.Application.Dtos;

public record ProductPriceChangeDto
{
    public string Id { get; }
    public int Price { get; }
    public string RestaurantId { get; }

    public ProductPriceChangeDto(string id, int price, string restaurantId)
    {
        Id = id;
        Price = price;
        RestaurantId = restaurantId;
    }
}
