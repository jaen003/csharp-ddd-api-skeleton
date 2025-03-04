using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public record RestaurantName : NonEmptyString
{
    private const string VALUE_OBJECT_NAME = "name";

    public RestaurantName(string value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.RESTAURANT) { }
}
