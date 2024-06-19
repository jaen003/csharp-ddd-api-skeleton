using Src.Core.Products.Domain.Repositories;
using Src.Core.Products.Domain.Exceptions;
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

    public async Task Validate(string productName, string restaurantId)
    {
        bool isNameAvailable = !await repository.ExistByStatusNotAndNameAndRestaurantId(
            ProductStatus.CreateDeleted(),
            new NonEmptyString(productName),
            new Uuid(restaurantId)
        );
        if (!isNameAvailable)
        {
            throw new ProductNameNotAvailable(productName);
        }
    }
}
