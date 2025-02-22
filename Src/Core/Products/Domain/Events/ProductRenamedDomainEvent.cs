using Src.Core.Shared.Domain.Events;

namespace Src.Core.Products.Domain.Events;

public class ProductRenamedDomainEvent : DomainEvent
{
    private const string ID_FIELD = "id";
    private const string NAME_FIELD = "name";

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
            new Guid(data[ID_FIELD].ToString()!),
            data[NAME_FIELD].ToString()!,
            eventId,
            timestamp
        );
    }

    public override Dictionary<string, object> ToPrimitives()
    {
        return new Dictionary<string, object> { { ID_FIELD, Id }, { NAME_FIELD, Name } };
    }
}
