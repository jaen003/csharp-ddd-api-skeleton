using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDeletedDomainEvent : DomainEvent
{
    private const string IdField = "id";

    public Guid Id { get; }

    public override string EventName => "product.deleted";

    public ProductDeletedDomainEvent() { }

    private ProductDeletedDomainEvent(Guid id, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
    }

    public ProductDeletedDomainEvent(Guid id)
    {
        Id = id;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductDeletedDomainEvent(
            new Guid(data[IdField].ToString()!),
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { IdField, Id } };
    }
}
