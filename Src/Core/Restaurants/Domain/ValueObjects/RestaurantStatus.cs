using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public class RestaurantStatus : NonNegativeShort
{
    private enum Type : short
    {
        Active,
        Deleted,
    }

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
        return new RestaurantStatus((short)Type.Active);
    }

    public static RestaurantStatus CreateDeleted()
    {
        return new RestaurantStatus((short)Type.Deleted);
    }

    public bool IsActive()
    {
        return Equals((short)Type.Active);
    }

    private bool IsValid()
    {
        return Equals((short)Type.Active) || Equals((short)Type.Deleted);
    }
}
