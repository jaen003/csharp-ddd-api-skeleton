using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class AllProductsFinder
{
    private readonly IProductRepository repository;

    public AllProductsFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<Dictionary<string, object>>> Find(
        string restaurantId,
        Pagination pagination
    )
    {
        List<Product> products = await repository.FindByStatusNotAndRestaurantIdAndPagination(
            ProductStatus.CreateDeleted(),
            new Uuid(restaurantId),
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
