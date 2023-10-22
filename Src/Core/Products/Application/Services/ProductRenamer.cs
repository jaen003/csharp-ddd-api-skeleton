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

    public async Task Rename(long id, string name, long restaurantId)
    {
        if (await IsProductNameCreatedInRestaurant(name, restaurantId))
        {
            throw new ProductNameAlreadyCreatedException(name);
        }
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new NonNegativeLongValueObject(id),
                new NonNegativeLongValueObject(restaurantId)
            ) ?? throw new ProductNotFoundException(id);
        string oldName = product.Name;
        product.Rename(name);
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product name '{oldName}' has been changed to '{name}'.");
    }

    async private Task<bool> IsProductNameCreatedInRestaurant(string name, long restaurantId)
    {
        return await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyStringValueObject(name),
            new NonNegativeLongValueObject(restaurantId)
        );
    }
}
