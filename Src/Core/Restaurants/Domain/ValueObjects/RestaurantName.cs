using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain.ValueObjects;

public record RestaurantName : NonEmptyString
{
    private const string ValueObjectName = "name";

    public RestaurantName(string value)
        : base(value, ValueObjectName, AggregateName.Restaurant) { }
}
