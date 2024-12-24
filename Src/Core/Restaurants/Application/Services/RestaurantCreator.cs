using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Application.Dtos;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Application.Logging;
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

    public async Task Create(RestaurantCreationDto creationDto)
    {
        if (!await IsRestaurantCreated(creationDto.Id))
        {
            Restaurant restaurant = Restaurant.Create(creationDto.Id, creationDto.Name);
            await repository.Save(restaurant);
            logger.Information($"The restaurant '{creationDto.Id}' has been created.");
        }
    }

    private async Task<bool> IsRestaurantCreated(string id)
    {
        return await repository.ExistsByStatusNotAndId(
            RestaurantStatus.CreateDeleted(),
            new Uuid(id)
        );
    }
}
