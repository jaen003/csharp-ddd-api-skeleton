namespace Src.Api.V1.InputModels.Products;

public class ProductCreationInputModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public int Price { get; set; }
    public required string Description { get; set; }
    public required Guid RestaurantId { get; set; }
}
