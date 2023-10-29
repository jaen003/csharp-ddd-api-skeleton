using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class InvalidRestaurantStatus : ValidationError
{
    private const int CODE = 102;

    public InvalidRestaurantStatus(short status)
        : base(CODE, $"The restaurant status '{status}' is invalid.") { }
}
