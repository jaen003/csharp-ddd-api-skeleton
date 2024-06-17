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
using Src.Core.Products.Application.Dtos;

namespace Src.Core.Products.Application.Services;

public class ProductCreator
{
    private readonly IProductRepository productRepository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductCreator(
        IProductRepository productRepository,
        IRestaurantRepository restaurantRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.productRepository = productRepository;
        this.restaurantRepository = restaurantRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Create(ProductCreationDto creationDto)
    {
        if (!await IsRestaurantCreated(creationDto.RestaurantId))
        {
            throw new RestaurantNotFound(creationDto.RestaurantId);
        }
        if (await IsProductNameCreatedInRestaurant(creationDto.Name, creationDto.RestaurantId))
        {
            throw new ProductNameNotAvailable(creationDto.Name);
        }
        Product product = Product.Create(
            creationDto.Id,
            creationDto.Name,
            creationDto.Price,
            creationDto.Description,
            creationDto.RestaurantId
        );
        await productRepository.Save(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{creationDto.Id}' has been created.");
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
        return await productRepository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyString(name),
            new Uuid(restaurantId)
        );
    }
}
