using System.ComponentModel.DataAnnotations;

namespace Src.Api.V1.Schemas.Products;

public class ProductDescriptionChangeSchema
{
    [Required]
    public string Description { get; set; } = null!;
}
