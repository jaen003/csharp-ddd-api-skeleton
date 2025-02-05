using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductPriceChangedDomainEvent : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string PRICE_FIELD = "price";

    public string Id { get; }
    public int Price { get; }
    public override string EventName => "product.price.changed";

    public ProductPriceChangedDomainEvent()
    {
        Id = string.Empty;
    }

    public ProductPriceChangedDomainEvent(string id, int price)
    {
        Id = id;
        Price = price;
    }

    private ProductPriceChangedDomainEvent(string id, int price, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Price = price;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductPriceChangedDomainEvent(
            data[ID_FIELD].ToString()!,
            int.Parse(data[PRICE_FIELD].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { ID_FIELD, Id }, { PRICE_FIELD, Price } };
    }
}
