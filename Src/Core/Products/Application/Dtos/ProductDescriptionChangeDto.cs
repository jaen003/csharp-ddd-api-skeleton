namespace Src.Core.Products.Application.Dtos;

public record ProductDescriptionChangeDto
{
    public Guid Id { get; }
    public string Description { get; }
    public Guid RestaurantId { get; }

    public ProductDescriptionChangeDto(Guid id, string description, Guid restaurantId)
    {
        Id = id;
        Description = description;
        RestaurantId = restaurantId;
    }
}
