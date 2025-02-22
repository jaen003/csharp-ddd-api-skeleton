using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductPriceChangedDomainEvent : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string PRICE_FIELD = "price";

    public Guid Id { get; }
    public int Price { get; }

    public override string EventName => "product.price.changed";

    public ProductPriceChangedDomainEvent() { }

    public ProductPriceChangedDomainEvent(Guid id, int price)
    {
        Id = id;
        Price = price;
    }

    private ProductPriceChangedDomainEvent(Guid id, int price, string eventId, int timestamp)
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
            new Guid(data[ID_FIELD].ToString()!),
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
