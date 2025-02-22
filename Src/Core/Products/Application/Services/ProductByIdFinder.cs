using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductByIdFinder
{
    private readonly IProductRepository repository;

    public ProductByIdFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ProductDto> Find(ProductByIdQueryDto queryDto)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                queryDto.Id,
                queryDto.RestaurantId
            ) ?? throw new ProductNotFoundException(queryDto.Id);
        return new ProductDto(product.Id, product.Name, product.Price, product.Description);
    }
}
