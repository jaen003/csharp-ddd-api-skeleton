using Src.Core.Restaurants.Application.Exceptions;
using Src.Core.Restaurants.Domain.ValueObjects;

namespace Src.Core.Restaurants.Application.Validators;

public class RestaurantExistenceValidator : IRestaurantExistenceValidator
{
    private readonly IRestaurantRepository repository;

    public RestaurantExistenceValidator(IRestaurantRepository repository)
    {
        this.repository = repository;
    }

    public async Task Validate(Guid restaurantId)
    {
        bool exists = await repository.ExistsByStatusNotAndId(
            RestaurantStatus.DELETED,
            restaurantId
        );
        if (!exists)
        {
            throw new RestaurantNotFoundException(restaurantId);
        }
    }
}
