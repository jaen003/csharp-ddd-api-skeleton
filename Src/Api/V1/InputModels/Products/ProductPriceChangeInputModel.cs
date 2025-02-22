namespace Src.Api.V1.InputModels.Products;

public class ProductPriceChangeInputModel
{
    public int Price { get; set; }
    public required Guid RestaurantId { get; set; }
}
