using Src.Core.Shared.Application.Paginations;

namespace Src.Core.Products.Application.Dtos;

public record AllProductsQueryDto
{
    public PaginationDto PaginationDto { get; }
    public Guid RestaurantId { get; }

    public AllProductsQueryDto(PaginationDto paginationDto, Guid restaurantId)
    {
        PaginationDto = paginationDto;
        RestaurantId = restaurantId;
    }
}
