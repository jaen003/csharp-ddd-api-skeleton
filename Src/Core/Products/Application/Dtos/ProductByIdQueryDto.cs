namespace Src.Core.Products.Application.Dtos;

public record ProductByIdQueryDto
{
    public Guid Id { get; }
    public Guid RestaurantId { get; }

    public ProductByIdQueryDto(Guid id, Guid restaurantId)
    {
        Id = id;
        RestaurantId = restaurantId;
    }
}
