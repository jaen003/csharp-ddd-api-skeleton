using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class RestaurantNotFound : ValidationError
{
    private const int CODE = 101;

    public RestaurantNotFound(long id)
        : base(CODE, $"The restaurant '{id}' has not been found.") { }
}
