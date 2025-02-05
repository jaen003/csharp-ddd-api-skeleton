using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDeletedDomainEvent : DomainEvent
{
    private const string ID_FIELD = "id";

    public string Id { get; }
    public override string EventName => "product.deleted";

    public ProductDeletedDomainEvent()
    {
        Id = string.Empty;
    }

    private ProductDeletedDomainEvent(string id, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
    }

    public ProductDeletedDomainEvent(string id)
    {
        Id = id;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductDeletedDomainEvent(data[ID_FIELD].ToString()!, eventId, timestamp);
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { ID_FIELD, Id } };
    }
}
