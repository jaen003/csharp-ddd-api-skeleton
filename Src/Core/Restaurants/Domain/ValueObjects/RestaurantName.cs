using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public class RestaurantName : NonEmptyStringValueObject
{
    public RestaurantName(string value)
        : base(value) { }
}
