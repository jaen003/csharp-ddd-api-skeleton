using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public class RestaurantStatus : NonNegativeShort
{
    private const short ACTIVE = 0;
    public const short DELETED = 1;

    public RestaurantStatus(short value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidRestaurantStatusException(value);
        }
    }

    public static RestaurantStatus CreateActive()
    {
        return new RestaurantStatus(ACTIVE);
    }

    public static RestaurantStatus CreateDeleted()
    {
        return new RestaurantStatus(DELETED);
    }

    public bool IsActive()
    {
        return Equals(ACTIVE);
    }

    private bool IsValid()
    {
        return Equals(ACTIVE) || Equals(DELETED);
    }
}
