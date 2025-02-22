using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application;

public interface IProductRepository
{
    Task Save(Product product);

    Task Update(Product product);

    Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        ProductStatus status,
        NonEmptyString name,
        Guid restaurantId
    );

    Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        ProductStatus status,
        Guid id,
        Guid restaurantId
    );

    Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        Guid restaurantId,
        Pagination pagination
    );
}
