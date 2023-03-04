using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductPriceChanged : DomainEvent
{
    public long Id { get; }
    public int Price { get; }
    public override string EventName => "product.price.changed";

    public ProductPriceChanged() { }

    public ProductPriceChanged(long id, int price)
    {
        Id = id;
        Price = price;
    }

    public ProductPriceChanged(long id, int price, string eventId, int timestamp)
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
        return new ProductPriceChanged(
            long.Parse(data["id"].ToString()!),
            int.Parse(data["price"].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id }, { "price", "" + Price }, };
    }
}
