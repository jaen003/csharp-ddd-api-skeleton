using System.ComponentModel.DataAnnotations;

namespace Src.Api.V1.Schemas.Products;

public class ProductPriceChangerSchema
{
    [Range(1, int.MaxValue)]
    public int Price { get; set; }
}
