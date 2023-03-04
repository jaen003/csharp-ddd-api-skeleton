using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Products.Application.Services;

public class ProductDeletor
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductDeletor(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Delete(ProductId id, RestaurantId restaurantId)
    {
        ProductStatus status = ProductStatus.CreateDeleted();
        Product? product = await repository.FindByStatusNotAndIdAndRestaurantId(
            status,
            id,
            restaurantId
        );
        if (product == null)
        {
            throw new ProductNotFoundException(id.Value);
        }
        product.Delete();
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{id.Value}' has been deleted.");
    }
}
