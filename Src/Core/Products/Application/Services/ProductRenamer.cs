using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Products.Application.Dtos;

namespace Src.Core.Products.Application.Services;

public class ProductRenamer
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly ProductNameAvailabilityValidator productNameAvailabilityValidator;

    public ProductRenamer(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        ProductNameAvailabilityValidator productNameAvailabilityValidator
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.productNameAvailabilityValidator = productNameAvailabilityValidator;
    }

    public async Task Rename(ProductNameChangeDto changeDto)
    {
        await productNameAvailabilityValidator.Validate(changeDto.Name, changeDto.RestaurantId);
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new Uuid(changeDto.Id),
                new Uuid(changeDto.RestaurantId)
            ) ?? throw new ProductNotFound(changeDto.Id);
        string oldName = product.Name;
        product.Rename(changeDto.Name);
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information($"The product name '{oldName}' has been changed to '{changeDto.Name}'.");
    }
}
