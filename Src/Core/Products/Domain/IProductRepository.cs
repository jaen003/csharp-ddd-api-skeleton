using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain;

public interface IProductRepository
{
    Task Save(Product product);

    Task Update(Product product);

    Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        ProductStatus status,
        NonEmptyString name,
        NonNegativeLong restaurantId
    );

    Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        ProductStatus status,
        NonNegativeLong id,
        NonNegativeLong restaurantId
    );

    Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        NonNegativeLong restaurantId,
        Pagination pagination
    );
}
