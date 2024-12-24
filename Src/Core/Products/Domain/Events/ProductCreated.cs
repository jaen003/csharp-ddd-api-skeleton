using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductCreated : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string NAME_FIELD = "name";
    private const string PRICE_FIELD = "price";
    private const string DESCRIPTION_FIELD = "description";

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

    private ProductCreated(
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
            data[ID_FIELD].ToString()!,
            data[NAME_FIELD].ToString()!,
            int.Parse(data[PRICE_FIELD].ToString()!),
            data[DESCRIPTION_FIELD].ToString()!,
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
        };
    }
}
