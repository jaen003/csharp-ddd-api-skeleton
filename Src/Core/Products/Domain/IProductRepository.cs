using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations;

namespace Src.Core.Products.Domain;

public interface IProductRepository
{
    Task Save(Product product);

    Task Update(Product product);

    Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        ProductStatus status,
        ProductName name,
        RestaurantId restaurantId
    );

    Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        ProductStatus status,
        ProductId id,
        RestaurantId restaurantId
    );

    Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        RestaurantId restaurantId,
        Pagination pagination
    );
}
