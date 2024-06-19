using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Products.Application.Dtos;
using Src.Core.Restaurants.Application.Services;

namespace Src.Core.Products.Application.Services;

public class ProductCreator
{
    private readonly IProductRepository productRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly RestaurantExistenceValidator restaurantExistenceValidator;
    private readonly ProductNameAvailabilityValidator productNameAvailabilityValidator;

    public ProductCreator(
        IProductRepository productRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        RestaurantExistenceValidator restaurantExistenceValidator,
        ProductNameAvailabilityValidator productNameAvailabilityValidator
    )
    {
        this.productRepository = productRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.restaurantExistenceValidator = restaurantExistenceValidator;
        this.productNameAvailabilityValidator = productNameAvailabilityValidator;
    }

    public async Task Create(ProductCreationDto creationDto)
    {
        await restaurantExistenceValidator.Validate(creationDto.RestaurantId);
        await productNameAvailabilityValidator.Validate(creationDto.Name, creationDto.RestaurantId);
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
}
