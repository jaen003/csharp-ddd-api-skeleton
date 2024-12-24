using Riok.Mapperly.Abstractions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Infrastructure.Models;

namespace Src.Core.Products.Infrastructure.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductModel ToModel(Product product);

    public partial Product ToEntity(ProductModel productModel);

    public partial List<Product> ToEntities(List<ProductModel> productModel);
}
