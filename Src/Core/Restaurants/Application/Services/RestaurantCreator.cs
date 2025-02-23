using Src.Core.Restaurants.Application.Dtos;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Application.Logging;

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

    private async Task<bool> IsRestaurantCreated(Guid id)
    {
        return await repository.ExistsByStatusNotAndId(RestaurantStatus.DELETED, id);
    }
}
