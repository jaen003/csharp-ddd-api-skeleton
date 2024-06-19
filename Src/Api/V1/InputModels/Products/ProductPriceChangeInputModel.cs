namespace Src.Api.V1.InputModels.Products;

public class ProductPriceChangeInputModel
{
    public int Price { get; set; }
    public required string RestaurantId { get; set; }
}
