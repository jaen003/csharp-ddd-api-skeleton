namespace Src.Core.Shared.Domain.Events;

public class DomainEventInformationCollection
{
    private readonly List<DomainEventInformation> items;

    public DomainEventInformationCollection()
    {
        items = new List<DomainEventInformation>();
    }

    public void Add(DomainEventInformation eventInformation)
    {
        items.Add(eventInformation);
    }

    public DomainEventInformation? GetFirst()
    {
        if (items.Count > 0)
        {
            return items[0];
        }
        return null;
    }

    public List<DomainEventInformation> GetAll()
    {
        return items;
    }
}
