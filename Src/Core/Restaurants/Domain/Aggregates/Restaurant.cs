using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Aggregates;

public class Restaurant
{
    public Guid Id { get; }
    private readonly RestaurantName name;
    private readonly RestaurantStatus status;

    public string Name => name.Value;
    public short Status => status.Value;

    public Restaurant(Guid id, string name, short status)
    {
        Id = id;
        this.name = new RestaurantName(name);
        this.status = new RestaurantStatus(status);
    }

    private Restaurant(
        Guid restaurantId,
        RestaurantName restaurantName,
        RestaurantStatus restaurantStatus
    )
    {
        Id = restaurantId;
        name = restaurantName;
        status = restaurantStatus;
    }

    public static Restaurant Create(Guid id, string name)
    {
        List<CustomException> exceptions = [];
        RestaurantName? restaurantName = null;
        try
        {
            restaurantName = new RestaurantName(name);
        }
        catch (CustomException exception)
        {
            exceptions.Add(exception);
        }
        if (exceptions.Count > 0)
        {
            throw new MultipleCustomException(exceptions.AsReadOnly());
        }
        return new(id, restaurantName!, RestaurantStatus.CreateActive());
    }
}
