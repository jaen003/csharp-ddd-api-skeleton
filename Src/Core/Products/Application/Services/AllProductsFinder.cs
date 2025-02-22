using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations.Aggregates;

namespace Src.Core.Products.Application.Services;

public class AllProductsFinder
{
    private readonly IProductRepository repository;

    public AllProductsFinder(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<List<ProductDto>> Find(AllProductsQueryDto queryDto)
    {
        Pagination pagination = Pagination.Create(
            queryDto.PaginationDto.Limit,
            queryDto.PaginationDto.StartIndex,
            queryDto.PaginationDto.SortingField,
            queryDto.PaginationDto.SortingType
        );
        List<Product> products = await repository.FindByStatusNotAndRestaurantIdAndPagination(
            ProductStatus.CreateDeleted(),
            queryDto.RestaurantId,
            pagination
        );
        List<ProductDto> result = new();
        foreach (Product product in products)
        {
            result.Add(new(product.Id, product.Name, product.Price, product.Description));
        }
        return result;
    }
}
