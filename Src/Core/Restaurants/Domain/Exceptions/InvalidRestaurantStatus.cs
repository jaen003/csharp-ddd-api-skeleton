using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Restaurants.Domain.Exceptions;

public class InvalidRestaurantStatus : DomainException
{
    public InvalidRestaurantStatus(short status)
        : base(
            CustomExceptionCode.InvalidRestaurantStatus,
            $"The restaurant status '{status}' is invalid."
        ) { }
}
