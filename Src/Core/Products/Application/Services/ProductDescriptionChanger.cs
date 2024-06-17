using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Products.Application.Dtos;

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

    public async Task Change(ProductDescriptionChangeDto changeDto)
    {
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.CreateDeleted(),
                new Uuid(changeDto.Id),
                new Uuid(changeDto.RestaurantId)
            ) ?? throw new ProductNotFound(changeDto.Id);
        string oldDescription = product.Description;
        product.ChangeDescription(changeDto.Description);
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product description '{oldDescription}' has been changed to "
                + $"'{changeDto.Description}'."
        );
    }
}
