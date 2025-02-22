using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Application.Services;

public class ProductNameAvailabilityValidator
{
    private readonly IProductRepository repository;

    public ProductNameAvailabilityValidator(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task Validate(string productName, Guid restaurantId)
    {
        bool isNameAvailable = !await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyString(productName),
            restaurantId
        );
        if (!isNameAvailable)
        {
            throw new ProductNameNotAvailableException(productName);
        }
    }
}
