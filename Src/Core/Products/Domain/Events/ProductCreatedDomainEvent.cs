using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductCreatedDomainEvent : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string NAME_FIELD = "name";
    private const string PRICE_FIELD = "price";
    private const string DESCRIPTION_FIELD = "description";
    private const string RESTAURANT_ID_FIELD = "restaurantId";

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
            new Guid(data[ID_FIELD].ToString()!),
            data[NAME_FIELD].ToString()!,
            int.Parse(data[PRICE_FIELD].ToString()!),
            data[DESCRIPTION_FIELD].ToString()!,
            new Guid(data[RESTAURANT_ID_FIELD].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object>
        {
            { ID_FIELD, Id },
            { NAME_FIELD, Name },
            { PRICE_FIELD, Price },
            { DESCRIPTION_FIELD, Description },
            { RESTAURANT_ID_FIELD, RestaurantId },
        };
    }
}
