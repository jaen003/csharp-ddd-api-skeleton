using System.ComponentModel.DataAnnotations;

namespace Src.Api.V1.Schemas.Products;

public class ProductNameChangeSchema
{
    [Required]
    public string Name { get; set; } = null!;
}
