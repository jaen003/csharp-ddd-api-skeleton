using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductDescriptionChanged : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string DESCRIPTION_FIELD = "description";

    public string Id { get; }
    public string Description { get; }
    public override string EventName => "product.description.changed";

    public ProductDescriptionChanged()
    {
        Description = string.Empty;
        Id = string.Empty;
    }

    private ProductDescriptionChanged(string id, string description, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Description = description;
    }

    public ProductDescriptionChanged(string id, string description)
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
            data[ID_FIELD].ToString()!,
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
            { DESCRIPTION_FIELD, Description },
        };
    }
}
