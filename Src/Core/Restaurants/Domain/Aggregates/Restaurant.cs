using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.Aggregates;

public class Restaurant
{
    private readonly NonNegativeLongValueObject id;
    private readonly NonEmptyStringValueObject name;
    private readonly RestaurantStatus status;

    public long Id
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

    public Restaurant(long id, string name, short status)
    {
        this.id = new NonNegativeLongValueObject(id);
        this.name = new NonEmptyStringValueObject(name);
        this.status = new RestaurantStatus(status);
    }

    public Restaurant(long id, string name, RestaurantStatus status)
    {
        this.id = new NonNegativeLongValueObject(id);
        this.name = new NonEmptyStringValueObject(name);
        this.status = status;
    }

    public static Restaurant Create(long id, string name)
    {
        return new(id, name, RestaurantStatus.CreateActived());
    }
}
