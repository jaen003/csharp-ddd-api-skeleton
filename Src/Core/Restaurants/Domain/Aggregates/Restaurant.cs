using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.Aggregates;

public class Restaurant
{
    private readonly Uuid id;
    private readonly NonEmptyString name;
    private readonly RestaurantStatus status;

    public string Id
    {
        get { return id.Value; }
    }
    public string Name
    {
        get { return name.Value; }
    }
    public short Status
    {
        get { return status.Value; }
    }

    public Restaurant(string id, string name, short status)
    {
        this.id = new Uuid(id);
        this.name = new NonEmptyString(name);
        this.status = new RestaurantStatus(status);
    }

    public Restaurant(string id, string name, RestaurantStatus status)
    {
        this.id = new Uuid(id);
        this.name = new NonEmptyString(name);
        this.status = status;
    }

    public static Restaurant Create(string id, string name)
    {
        return new(id, name, RestaurantStatus.CreateActived());
    }
}
