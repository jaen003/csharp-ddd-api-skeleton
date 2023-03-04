using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDeleted : DomainEvent
{
    public long Id { get; }
    public override string EventName => "product.deleted";

    public ProductDeleted() { }

    public ProductDeleted(long id, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
    }

    public ProductDeleted(long id)
    {
        Id = id;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductDeleted(long.Parse(data["id"].ToString()!), eventId, timestamp);
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id } };
    }
}
