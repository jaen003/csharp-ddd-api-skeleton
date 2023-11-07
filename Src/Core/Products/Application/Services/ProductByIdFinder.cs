using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductByIdFinder
{
    private readonly IProductRepository repository;

    public ProductByIdFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Dictionary<string, object>> Find(string id, string restaurantId)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new Uuid(id),
                new Uuid(restaurantId)
            ) ?? throw new ProductNotFound(id);
        return new()
        {
            { "name", product.Name },
            { "price", product.Price },
            { "description", product.Description }
        };
    }
}
