using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Products.Application.Dtos;

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
                new Uuid(queryDto.Id),
                new Uuid(queryDto.RestaurantId)
            ) ?? throw new ProductNotFound(queryDto.Id);
        return new ProductDto(product.Id, product.Name, product.Price, product.Description);
    }
}
