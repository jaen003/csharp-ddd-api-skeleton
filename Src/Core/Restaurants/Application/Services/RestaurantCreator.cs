using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Restaurants.Application.Services;

public class RestaurantCreator
{
    private readonly IRestaurantRepository repository;
    private readonly ILogger logger;

    public RestaurantCreator(IRestaurantRepository repository, ILogger logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task Create(RestaurantId id, RestaurantName name)
    {
        if (!await IsRestaurantCreated(id))
        {
            Restaurant restaurant = Restaurant.Create(id, name);
            await repository.Save(restaurant);
            logger.Information($"The restaurant '{id.Value}' has been created.");
        }
    }

    async private Task<bool> IsRestaurantCreated(RestaurantId id)
    {
        RestaurantStatus status = RestaurantStatus.CreateDeleted();
        return await repository.ExistsByStatusNotAndId(status, id);
    }
}
