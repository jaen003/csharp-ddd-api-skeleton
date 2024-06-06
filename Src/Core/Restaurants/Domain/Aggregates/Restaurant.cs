using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.Aggregates;

public class Restaurant
{
    private readonly Uuid id;
    private readonly NonEmptyString name;
    private readonly RestaurantStatus status;

    public string Id => id.Value;
    public string Name => name.Value;
    public short Status => status.Value;

    public Restaurant(string id, string name, short status)
    {
        this.id = new Uuid(id);
        this.name = new NonEmptyString(name);
        this.status = new RestaurantStatus(status);
    }

    private Restaurant(
        Uuid restaurantId,
        NonEmptyString restaurantName,
        RestaurantStatus restaurantStatus
    )
    {
        id = restaurantId;
        name = restaurantName;
        status = restaurantStatus;
    }

    public static Restaurant Create(string id, string name)
    {
        List<CustomException> exceptions = new();
        Uuid? restaurantId = null;
        NonEmptyString? restaurantName = null;
        try
        {
            restaurantId = new Uuid(id);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        try
        {
            restaurantName = new NonEmptyString(name);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        if (exceptions.Count > 0)
        {
            throw new MultipleCustomException(exceptions);
        }
        return new(restaurantId!, restaurantName!, RestaurantStatus.CreateActive());
    }
}
