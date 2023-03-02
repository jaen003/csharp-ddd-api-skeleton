using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public class RestaurantId : NonNegativeLongValueObject
{
    public RestaurantId(long value)
        : base(value) { }
}
