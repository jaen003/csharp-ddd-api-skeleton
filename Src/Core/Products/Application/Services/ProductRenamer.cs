using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Products.Application.Services;

public class ProductRenamer
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductRenamer(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Rename(ProductId id, ProductName name, RestaurantId restaurantId)
    {
        if (await IsProductNameCreatedInRestaurant(name, restaurantId))
        {
            throw new ProductNameAlreadyCreatedException(name.Value);
        }
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
        ProductName oldName = product.Name;
        product.Rename(name);
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product name '{oldName.Value}' has been changed to '{name.Value}'."
        );
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
