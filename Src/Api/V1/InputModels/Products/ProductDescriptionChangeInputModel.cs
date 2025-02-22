namespace Src.Api.V1.InputModels.Products;

public class ProductDescriptionChangeInputModel
{
    public required string Description { get; set; }
    public required Guid RestaurantId { get; set; }
}
