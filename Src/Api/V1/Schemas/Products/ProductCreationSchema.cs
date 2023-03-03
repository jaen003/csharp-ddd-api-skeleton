using System.ComponentModel.DataAnnotations;

namespace Src.Api.V1.Schemas.Products;

public class ProductCreationSchema
{
    [Required]
    public string Name { get; set; } = null!;

    [Range(1, int.MaxValue)]
    public int Price { get; set; }

    [Required]
    public string Description { get; set; } = null!;
}
