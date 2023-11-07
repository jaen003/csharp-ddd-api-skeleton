using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Logging;
using Src.Core.Shared.Domain.ValueObjects;

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

    public async Task Rename(string id, string name, string restaurantId)
    {
        if (await IsProductNameCreatedInRestaurant(name, restaurantId))
        {
            throw new ProductNameNotAvailable(name);
        }
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new Uuid(id),
                new Uuid(restaurantId)
            ) ?? throw new ProductNotFound(id);
        string oldName = product.Name;
        product.Rename(name);
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product name '{oldName}' has been changed to '{name}'.");
    }

    async private Task<bool> IsProductNameCreatedInRestaurant(string name, string restaurantId)
    {
        return await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyString(name),
            new Uuid(restaurantId)
        );
    }
}
