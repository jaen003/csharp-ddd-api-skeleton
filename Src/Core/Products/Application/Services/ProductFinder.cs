using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations;

namespace Src.Core.Products.Application.Services;

public class ProductFinder
{
    private readonly IProductRepository repository;

    public ProductFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Dictionary<string, object>> FindByIdAndResturantId(
        ProductId id,
        RestaurantId restaurantId
    )
    {
        ProductStatus status = ProductStatus.CreateDeleted();
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(status, id, restaurantId)
            ?? throw new ProductNotFoundException(id.Value);
        return new()
        {
            { "name", product.Name.Value },
            { "price", product.Price.Value },
            { "description", product.Description.Value }
        };
    }

    public async Task<List<Dictionary<string, object>>> FindByResturantIdAndPagination(
        RestaurantId restaurantId,
        Pagination pagination
    )
    {
        ProductStatus status = ProductStatus.CreateDeleted();
        List<Product> products = await repository.FindByStatusNotAndRestaurantIdAndPagination(
            status,
            restaurantId,
            pagination
        );
        List<Dictionary<string, object>> result = new();
        foreach (Product product in products)
        {
            result.Add(
                new()
                {
                    { "id", product.Id.Value },
                    { "name", product.Name.Value },
                    { "price", product.Price.Value }
                }
            );
        }
        return result;
    }
}
