namespace Src.Core.Products.Application.Dtos;

public record ProductCreationData
{
    public Guid Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }
    public Guid RestaurantId { get; }

    public ProductCreationData(
        Guid id,
        string name,
        int price,
        string description,
        Guid restaurantId
    )
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        RestaurantId = restaurantId;
    }
}
