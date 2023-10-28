using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Logging;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductDescriptionChanger
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductDescriptionChanger(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Change(long id, string description, long restaurantId)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new NonNegativeLong(id),
                new NonNegativeLong(restaurantId)
            ) ?? throw new ProductNotFound(id);
        string oldDescription = product.Description;
        product.ChangeDescription(description);
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product description '{oldDescription}' has been changed to " + $"'{description}'."
        );
    }
}
