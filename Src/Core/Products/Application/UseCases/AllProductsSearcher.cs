using System.Collections.ObjectModel;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Paginations.Aggregates;

namespace Src.Core.Products.Application.UseCases;

public class AllProductsSearcher
{
    private readonly IProductRepository repository;

    public AllProductsSearcher(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task<ReadOnlyCollection<ProductResponseData>> Search(
        AllProductsSearchData searchData
    )
    {
        Pagination pagination = Pagination.Create(
            searchData.PaginationData.Limit,
            searchData.PaginationData.StartIndex,
            searchData.PaginationData.SortingField,
            searchData.PaginationData.SortingType
        );
        List<Product> products = await repository.FindByStatusNotAndRestaurantIdAndPagination(
            ProductStatus.DELETED,
            searchData.RestaurantId,
            pagination
        );
        List<ProductResponseData> result = [];
        foreach (Product product in products)
        {
            result.Add(new(product.Id, product.Name, product.Price, product.Description));
        }
        return result.AsReadOnly();
    }
}
