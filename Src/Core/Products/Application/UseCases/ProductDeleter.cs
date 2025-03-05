using Src.Core.Products.Application.DTOs;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Products.Application.UseCases;

public class ProductDeleter
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductDeleter(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Delete(ProductDeletionData deletionData)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.DELETED,
                deletionData.Id,
                deletionData.RestaurantId
            ) ?? throw new ProductNotFoundException(deletionData.Id);
        product.Delete();
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product '{deletionData.Id}' has been deleted.");
    }
}
