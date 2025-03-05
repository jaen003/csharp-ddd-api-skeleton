using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Products.Application.UseCases;

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

    public async Task Change(ProductDescriptionChangeData changeData)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.DELETED,
                changeData.Id,
                changeData.RestaurantId
            ) ?? throw new ProductNotFoundException(changeData.Id);
        string oldDescription = product.Description;
        product.ChangeDescription(changeData.Description);
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product description '{oldDescription}' has been changed to "
                + $"'{changeData.Description}'."
        );
    }
}
