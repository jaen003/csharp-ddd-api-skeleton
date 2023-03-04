using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDescriptionChanged : DomainEvent
{
    public long Id { get; }
    public string Description { get; }
    public override string EventName => "product.description.changed";

    public ProductDescriptionChanged()
    {
        Description = string.Empty;
    }

    public ProductDescriptionChanged(long id, string description, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Description = description;
    }

    public ProductDescriptionChanged(long id, string description)
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
        return new ProductDescriptionChanged(
            long.Parse(data["id"].ToString()!),
            data["description"].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id }, { "description", "" + Description } };
    }
}
