using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class RestaurantNotFoundException : DomainException
{
    public RestaurantNotFoundException(string id)
        : base(CustomExceptionCode.RestaurantNotFound, $"The restaurant '{id}' has not been found.")
    { }
}
