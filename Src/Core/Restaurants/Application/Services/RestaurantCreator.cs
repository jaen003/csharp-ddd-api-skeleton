using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Logging;
using Src.Core.Shared.Domain.ValueObjects;

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

    public async Task Create(string id, string name)
    {
        if (!await IsRestaurantCreated(id))
        {
            Restaurant restaurant = Restaurant.Create(id, name);
            await repository.Save(restaurant);
            logger.Information($"The restaurant '{id}' has been created.");
        }
    }

    async private Task<bool> IsRestaurantCreated(string id)
    {
        return await repository.ExistsByStatusNotAndId(
            RestaurantStatus.CreateDeleted(),
            new Uuid(id)
        );
    }
}
