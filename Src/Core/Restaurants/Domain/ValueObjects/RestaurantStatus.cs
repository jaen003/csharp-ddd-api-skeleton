using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public class RestaurantStatus : NonNegativeShort
{
    private const short ACTIVED = 1;
    private const short DELETED = 2;

    public RestaurantStatus(short value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidRestaurantStatus(value);
        }
    }

    public static RestaurantStatus CreateActived()
    {
        return new RestaurantStatus(ACTIVED);
    }

    public static RestaurantStatus CreateDeleted()
    {
        return new RestaurantStatus(DELETED);
    }

    public bool IsActived()
    {
        return Equals(ACTIVED);
    }

    private bool IsValid()
    {
        return Equals(ACTIVED) || Equals(DELETED);
    }
}
