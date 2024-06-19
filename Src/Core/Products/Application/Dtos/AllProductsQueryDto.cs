using Src.Core.Shared.Application.Paginations;

namespace Src.Core.Products.Application.Dtos;

public record AllProductsQueryDto
{
    public PaginationDto PaginationDto { get; }
    public string RestaurantId { get; }

    public AllProductsQueryDto(PaginationDto paginationDto, string restaurantId)
    {
        PaginationDto = paginationDto;
        RestaurantId = restaurantId;
    }
}
