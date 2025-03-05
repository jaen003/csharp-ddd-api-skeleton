using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Products.Application.UseCases;

public class ProductPriceChanger
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    public ProductPriceChanger(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    public async Task Change(ProductPriceChangeData changeData)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.DELETED,
                changeData.Id,
                changeData.RestaurantId
            ) ?? throw new ProductNotFoundException(changeData.Id);
        int oldPrice = product.Price;
        product.ChangePrice(changeData.Price);
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product price '{oldPrice}' has been changed to '{changeData.Price}'."
        );
    }
}
