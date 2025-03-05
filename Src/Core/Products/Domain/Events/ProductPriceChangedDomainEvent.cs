using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductPriceChangedDomainEvent : DomainEvent
{
    private const string IdField = "id";
    private const string PriceField = "price";

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
            new Guid(data[IdField].ToString()!),
            int.Parse(data[PriceField].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { IdField, Id }, { PriceField, Price } };
    }
}
