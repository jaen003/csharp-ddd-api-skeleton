namespace Src.Api.V1.InputModels.Products;

public class ProductNameChangeInputModel
{
    public required string Name { get; set; }
    public required Guid RestaurantId { get; set; }
}
