using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class InvalidRestaurantStatusException : DomainException
{
    public InvalidRestaurantStatusException(short status)
        : base(
            CustomExceptionCode.InvalidRestaurantStatus,
            $"The restaurant status '{status}' is invalid."
        ) { }
}
