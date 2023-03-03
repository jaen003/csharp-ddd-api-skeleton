using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class RestaurantNotFoundException : ValidationErrorException
{
    private const int CODE = 101;

    public RestaurantNotFoundException(int code, string message)
        : base(code, message) { }

    public RestaurantNotFoundException() { }

    public RestaurantNotFoundException(long id)
        : base(CODE, $"The restaurant '{id}' has not been found.") { }

    public RestaurantNotFoundException(string? message)
        : base(message) { }

    public RestaurantNotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public RestaurantNotFoundException(int code, int type, string message)
        : base(code, type, message) { }
}
