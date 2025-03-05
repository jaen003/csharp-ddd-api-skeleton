using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductRenamedDomainEvent : DomainEvent
{
    private const string IdField = "id";
    private const string NameField = "name";

    public Guid Id { get; }
    public string Name { get; }

    public override string EventName => "product.renamed";

    public ProductRenamedDomainEvent()
    {
        Name = string.Empty;
    }

    private ProductRenamedDomainEvent(Guid id, string name, string eventId, int timestamp)
        : base(eventId, timestamp)
    {
        Id = id;
        Name = name;
    }

    public ProductRenamedDomainEvent(Guid id, string name)
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
        return new ProductRenamedDomainEvent(
            new Guid(data[IdField].ToString()!),
            data[NameField].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { IdField, Id }, { NameField, Name } };
    }
}
