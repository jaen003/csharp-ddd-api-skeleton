using Src.Core.Restaurants.Application.Exceptions;
using Src.Core.Restaurants.Domain.ValueObjects;

namespace Src.Core.Restaurants.Application.Services;

public class RestaurantExistenceValidator
{
    private readonly IRestaurantRepository repository;

    public RestaurantExistenceValidator(IRestaurantRepository restaurantRepository)
    {
        this.repository = restaurantRepository;
    }

    public async Task Validate(Guid restaurantId)
    {
        bool exists = await repository.ExistsByStatusNotAndId(
            RestaurantStatus.CreateDeleted(),
            restaurantId
        );
        if (!exists)
        {
            throw new RestaurantNotFoundException(restaurantId);
        }
    }
}
