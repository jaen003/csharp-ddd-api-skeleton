using Src.Core.Products.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations.Aggregates;

namespace Src.Core.Products.Application;

public interface IProductRepository
{
    Task Save(Product product);

    Task Update(Product product);

    Task<bool> ExistByStatusNotAndNameAndRestaurantId(short status, string name, Guid restaurantId);

    Task<Product?> FindByStatusNotAndIdAndRestaurantId(short status, Guid id, Guid restaurantId);

    Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        short status,
        Guid restaurantId,
        Pagination pagination
    );
}
