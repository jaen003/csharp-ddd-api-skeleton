using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Domain.ValueObjects;

namespace Src.Core.Products.Application.Validators;

public class ProductNameAvailabilityValidator : IProductNameAvailabilityValidator
{
    private readonly IProductRepository repository;

    public ProductNameAvailabilityValidator(IProductRepository repository)
    {
        this.repository = repository;
    }

    public async Task Validate(string productName, Guid restaurantId)
    {
        bool isNameAvailable = !await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.DELETED,
            productName,
            restaurantId
        );
        if (!isNameAvailable)
        {
            throw new ProductNameNotAvailableException(productName);
        }
    }
}
