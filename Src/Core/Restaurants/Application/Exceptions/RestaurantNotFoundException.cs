using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Application.Exceptions;

public class RestaurantNotFoundException : CustomApplicationException
{
    public RestaurantNotFoundException(string id)
        : base(CustomExceptionCode.RestaurantNotFound, $"The restaurant '{id}' has not been found.")
    { }
}
