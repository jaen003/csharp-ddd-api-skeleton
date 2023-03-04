using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductRenamed : DomainEvent
{
    public long Id { get; }
    public string Name { get; }
    public override string EventName => "product.renamed";

    public ProductRenamed()
    {
        Name = string.Empty;
    }

    public ProductRenamed(long id, string name, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
    }

    public ProductRenamed(long id, string name)
    {
        Id = id;
        Name = name;
    }

    public override DomainEvent FromPrimitives(
        string eventId,
        int timestamp,
        Dictionary<string, object> data
    )
    {
        return new ProductRenamed(
            long.Parse(data["id"].ToString()!),
            data["name"].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { "id", Id }, { "name", "" + Name } };
    }
}
