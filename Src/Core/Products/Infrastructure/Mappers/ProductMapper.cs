using Riok.Mapperly.Abstractions;
using ProductModel = Src.Core.Products.Infrastructure.Models.Product;
using Src.Core.Products.Domain.Aggregates;

namespace Src.Core.Products.Infrastructure.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductModel ToModel(Product product);

    public partial Product ToEntity(ProductModel productModel);

    public partial List<Product> ToEntities(List<ProductModel> productModel);
}
