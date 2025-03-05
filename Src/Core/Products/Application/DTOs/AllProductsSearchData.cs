using Src.Core.Shared.Application.Paginations.DTOs;

namespace Src.Core.Products.Application.DTOs;

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
