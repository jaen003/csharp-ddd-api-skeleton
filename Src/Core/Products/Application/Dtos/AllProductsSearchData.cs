using Src.Core.Shared.Application.Paginations.Dtos;

namespace Src.Core.Products.Application.Dtos;

public record AllProductsSearchData
{
    public PaginationData PaginationData { get; }
    public Guid RestaurantId { get; }

    public AllProductsSearchData(PaginationData paginationData, Guid restaurantId)
    {
        PaginationData = paginationData;
        RestaurantId = restaurantId;
    }
}
