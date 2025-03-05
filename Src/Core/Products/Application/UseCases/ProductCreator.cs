using Src.Core.Products.Application.DTOs;
using Src.Core.Products.Application.Validators;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Restaurants.Application.Validators;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Products.Application.UseCases;

public class ProductCreator
{
    private readonly IProductRepository productRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly IRestaurantExistenceValidator restaurantExistenceValidator;
    private readonly IProductNameAvailabilityValidator productNameAvailabilityValidator;

    public ProductCreator(
        IProductRepository productRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        IRestaurantExistenceValidator restaurantExistenceValidator,
        IProductNameAvailabilityValidator productNameAvailabilityValidator
    )
    {
        this.productRepository = productRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.restaurantExistenceValidator = restaurantExistenceValidator;
        this.productNameAvailabilityValidator = productNameAvailabilityValidator;
    }

    public async Task Create(ProductCreationData creationData)
    {
        await restaurantExistenceValidator.Validate(creationData.RestaurantId);
        await productNameAvailabilityValidator.Validate(
            creationData.Name,
            creationData.RestaurantId
        );
        Product product = Product.Create(
            creationData.Id,
            creationData.Name,
            creationData.Price,
            creationData.Description,
            creationData.RestaurantId
        );
        await productRepository.Save(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{creationData.Id}' has been created.");
    }
}
