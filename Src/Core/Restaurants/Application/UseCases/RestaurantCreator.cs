using Src.Core.Restaurants.Application.Dtos;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Restaurants.Application.UseCases;

public class RestaurantCreator
{
    private readonly IRestaurantRepository repository;
    private readonly ILogger logger;

    public RestaurantCreator(IRestaurantRepository repository, ILogger logger)
    {
        this.repository = repository;
        this.logger = logger;
    }

    public async Task Create(RestaurantCreationData creationData)
    {
        if (!await IsRestaurantCreated(creationData.Id))
        {
            Restaurant restaurant = Restaurant.Create(creationData.Id, creationData.Name);
            await repository.Save(restaurant);
            logger.Information($"The restaurant '{creationData.Id}' has been created.");
        }
    }

    private async Task<bool> IsRestaurantCreated(Guid id)
    {
        return await repository.ExistsByStatusNotAndId(RestaurantStatus.DELETED, id);
    }
}
