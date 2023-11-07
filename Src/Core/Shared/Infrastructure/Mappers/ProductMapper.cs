using Riok.Mapperly.Abstractions;
using ProductModel = Src.Core.Shared.Infrastructure.Database.Models.Product;
using Src.Core.Products.Domain.Aggregates;

namespace Src.Core.Shared.Infrastructure.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductModel ToModel(Product product);

    public partial Product ToEntity(ProductModel productModel);

    public partial List<Product> ToEntities(List<ProductModel> productModel);
}
