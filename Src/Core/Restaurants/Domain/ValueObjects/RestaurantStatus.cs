using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public record RestaurantStatus : NonNegativeShort
{
    private const short Active = 0;
    public const short Deleted = 1;
    private const string ValueObjectName = "status";

    public RestaurantStatus(short value)
        : base(value, ValueObjectName, AggregateName.Restaurant)
    {
        if (!IsValid())
        {
            throw new InvalidRestaurantStatusException(value);
        }
    }

    public static RestaurantStatus CreateActive()
    {
        return new RestaurantStatus(Active);
    }

    public static RestaurantStatus CreateDeleted()
    {
        return new RestaurantStatus(Deleted);
    }

    public bool IsActive()
    {
        return Equals(Active);
    }

    private bool IsValid()
    {
        return Equals(Active) || Equals(Deleted);
    }
}
