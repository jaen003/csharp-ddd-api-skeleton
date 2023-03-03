using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Products.Application.Services;

public class ProductCreator
{
    private readonly IProductRepository repository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly IIdentifierGenerator identifierGenerator;

    public ProductCreator(
        IProductRepository repository,
        IRestaurantRepository restaurantRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        IIdentifierGenerator identifierGenerator
    )
    {
        this.repository = repository;
        this.restaurantRepository = restaurantRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.identifierGenerator = identifierGenerator;
    }

    public async Task Create(
        ProductName name,
        ProductPrice price,
        ProductDescription description,
        RestaurantId restaurantId
    )
    {
        if (!await IsRestaurantCreated(restaurantId))
        {
            throw new RestaurantNotFoundException(restaurantId.Value);
        }
        if (await IsProductNameCreatedInRestaurant(name, restaurantId))
        {
            throw new ProductNameAlreadyCreatedException(name.Value);
        }
        ProductId id = new(identifierGenerator.Generate());
        Product product = Product.Create(id, name, price, description, restaurantId);
        await repository.Save(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{id.Value}' has been created.");
    }

    async private Task<bool> IsRestaurantCreated(RestaurantId id)
    {
        RestaurantStatus status = RestaurantStatus.CreateDeleted();
        return await restaurantRepository.ExistsByStatusNotAndId(status, id);
    }

    async private Task<bool> IsProductNameCreatedInRestaurant(
        ProductName name,
        RestaurantId restaurantId
    )
    {
        ProductStatus status = ProductStatus.CreateDeleted();
        return await repository.ExistByStatusNotAndNameAndRestaurantId(status, name, restaurantId);
    }
}
