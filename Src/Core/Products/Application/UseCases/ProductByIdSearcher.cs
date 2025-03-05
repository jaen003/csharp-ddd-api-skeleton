using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;

namespace Src.Core.Products.Application.UseCases;

public class ProductByIdSearcher
{
    private readonly IProductRepository repository;

    public ProductByIdSearcher(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ProductResponseData> Search(ProductSearchData searchData)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.DELETED,
                searchData.Id,
                searchData.RestaurantId
            ) ?? throw new ProductNotFoundException(searchData.Id);
        return new ProductResponseData(
            product.Id,
            product.Name,
            product.Price,
            product.Description
        );
    }
}
