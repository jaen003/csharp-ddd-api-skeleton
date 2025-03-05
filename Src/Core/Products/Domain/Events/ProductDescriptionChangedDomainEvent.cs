using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDescriptionChangedDomainEvent : DomainEvent
{
    private const string IdField = "id";
    private const string DescriptionField = "description";

    public Guid Id { get; }
    public string Description { get; }

    public override string EventName => "product.description.changed";

    public ProductDescriptionChangedDomainEvent()
    {
        Description = string.Empty;
    }

    private ProductDescriptionChangedDomainEvent(
        Guid id,
        string description,
        string eventId,
        int timestamp
    )
        : base(eventId, timestamp)
    {
        Id = id;
        Description = description;
    }

    public ProductDescriptionChangedDomainEvent(Guid id, string description)
    {
        Id = id;
        Description = description;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductDescriptionChangedDomainEvent(
            new Guid(data[IdField].ToString()!),
            data[DescriptionField].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object>
        {
            { IdField, Id },
            { DescriptionField, Description },
        };
    }
}
