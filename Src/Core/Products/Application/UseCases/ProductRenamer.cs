using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Application.Validators;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;

namespace Src.Core.Products.Application.UseCases;

public class ProductRenamer
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly IProductNameAvailabilityValidator productNameAvailabilityValidator;

    public ProductRenamer(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        IProductNameAvailabilityValidator productNameAvailabilityValidator
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.productNameAvailabilityValidator = productNameAvailabilityValidator;
    }

    public async Task Rename(ProductNameChangeData changeData)
    {
        await productNameAvailabilityValidator.Validate(changeData.Name, changeData.RestaurantId);
        Product? product =
            await repository.FindByStatusNotAndIdAndRestaurantId(
                ProductStatus.DELETED,
                changeData.Id,
                changeData.RestaurantId
            ) ?? throw new ProductNotFoundException(changeData.Id);
        string oldName = product.Name;
        product.Rename(changeData.Name);
        await repository.Update(product);
        await eventPublisher.Publish(product.PullEvents());
        logger.Information(
            $"The product name '{oldName}' has been changed to '{changeData.Name}'."
        );
    }
}
