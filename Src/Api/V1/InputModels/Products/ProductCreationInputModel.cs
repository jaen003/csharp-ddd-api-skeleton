namespace Src.Api.V1.InputModels.Products;

public class ProductCreationInputModel
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public int Price { get; set; }
    public required string Description { get; set; }
    public required string RestaurantId { get; set; }
}
