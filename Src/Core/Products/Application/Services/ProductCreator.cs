using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.Repositories;
using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductCreator
{
    private readonly IProductRepository repository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductCreator(
        IProductRepository repository,
        IRestaurantRepository restaurantRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.restaurantRepository = restaurantRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Create(
        string id,
        string name,
        int price,
        string description,
        string restaurantId
    )
    {
        if (!await IsRestaurantCreated(restaurantId))
        {
            throw new RestaurantNotFound(restaurantId);
        }
        if (await IsProductNameCreatedInRestaurant(name, restaurantId))
        {
            throw new ProductNameNotAvailable(name);
        }
        Product product = Product.Create(id, name, price, description, restaurantId);
        await repository.Save(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{id}' has been created.");
    }

    private async Task<bool> IsRestaurantCreated(string restaurantId)
    {
        return await restaurantRepository.ExistsByStatusNotAndId(
            RestaurantStatus.CreateDeleted(),
            new Uuid(restaurantId)
        );
    }

    private async Task<bool> IsProductNameCreatedInRestaurant(string name, string restaurantId)
    {
        return await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyString(name),
            new Uuid(restaurantId)
        );
    }
}
