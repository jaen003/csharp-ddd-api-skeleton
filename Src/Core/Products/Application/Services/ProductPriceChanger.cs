using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Logging;

namespace Src.Core.Products.Application.Services;

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

    public async Task Change(ProductId id, ProductPrice price, RestaurantId restaurantId)
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
        ProductPrice oldPrice = product.Price;
        product.ChangePrice(price);
        await repository.Update(product);
        eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product price '{oldPrice.Value}' has been changed to '{price.Value}'."
        );
    }
}
