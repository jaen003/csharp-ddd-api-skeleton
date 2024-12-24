using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Restaurants.Domain.Repositories;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Application.Services;

public class RestaurantExistenceValidator
{
    private readonly IRestaurantRepository repository;

    public RestaurantExistenceValidator(IRestaurantRepository restaurantRepository)
    {
        this.repository = restaurantRepository;
    }

    public async Task Validate(string restaurantId)
    {
        bool exists = await repository.ExistsByStatusNotAndId(
            RestaurantStatus.CreateDeleted(),
            new Uuid(restaurantId)
        );
        if (!exists)
        {
            throw new RestaurantNotFound(restaurantId);
        }
    }
}
