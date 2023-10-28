using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductFinder
{
    private readonly IProductRepository repository;

    public ProductFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Dictionary<string, object>> FindByIdAndResturantId(long id, long restaurantId)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new NonNegativeLong(id),
                new NonNegativeLong(restaurantId)
            ) ?? throw new ProductNotFound(id);
        return new()
        {
            { "name", product.Name },
            { "price", product.Price },
            { "description", product.Description }
        };
    }

    public async Task<List<Dictionary<string, object>>> FindByResturantIdAndPagination(
        long restaurantId,
        Pagination pagination
    )
    {
        List<Product> products = await repository.FindByStatusNotAndRestaurantIdAndPagination(
            ProductStatus.CreateDeleted(),
            new NonNegativeLong(restaurantId),
            pagination
        );
        List<Dictionary<string, object>> result = new();
        foreach (Product product in products)
        {
            result.Add(
                new() { { "id", product.Id }, { "name", product.Name }, { "price", product.Price } }
            );
        }
        return result;
    }
}
