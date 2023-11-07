using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductCreated : DomainEvent
{
    public string Id { get; }
    public string Name { get; }
    public int Price { get; }
    public string Description { get; }
    public override string EventName => "product.created";

    public ProductCreated()
    {
        Id = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
    }

    public ProductCreated(
        string id,
        string name,
        int price,
        string description,
        string eventId,
        int timestamp
    )
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public ProductCreated(string id, string name, int price, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductCreated(
            data["id"].ToString()!,
            data["name"].ToString()!,
            int.Parse(data["price"].ToString()!),
            data["description"].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object>
        {
            { "id", Id },
            { "name", Name },
            { "price", Price },
            { "description", Description }
        };
    }
}
