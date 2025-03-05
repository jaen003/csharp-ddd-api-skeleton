using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductCreatedDomainEvent : DomainEvent
{
    private const string IdField = "id";
    private const string NameField = "name";
    private const string PriceField = "price";
    private const string DescriptionField = "description";
    private const string RestaurantIdField = "restaurantId";

    public Guid Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }
    public Guid RestaurantId { get; }

    public override string EventName => "product.created";

    public ProductCreatedDomainEvent()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    private ProductCreatedDomainEvent(
        Guid id,
        string name,
        int price,
        string description,
        Guid restaurantId,
        string eventId,
        int timestamp
    )
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        RestaurantId = restaurantId;
    }

    public ProductCreatedDomainEvent(
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

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductCreatedDomainEvent(
            new Guid(data[IdField].ToString()!),
            data[NameField].ToString()!,
            int.Parse(data[PriceField].ToString()!),
            data[DescriptionField].ToString()!,
            new Guid(data[RestaurantIdField].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object>
        {
            { IdField, Id },
            { NameField, Name },
            { PriceField, Price },
            { DescriptionField, Description },
            { RestaurantIdField, RestaurantId },
        };
    }
}
