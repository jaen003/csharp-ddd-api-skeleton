namespace Src.Core.Products.Application.Dtos;

public record ProductNameChangeData
{
    public Guid Id { get; }
    public string Name { get; }
    public Guid RestaurantId { get; }

    public ProductNameChangeData(Guid id, string name, Guid restaurantId)
    {
        Id = id;
        Name = name;
        RestaurantId = restaurantId;
    }
}
