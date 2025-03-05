using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Application.Validators;

namespace Tests.Products;

public class ProductNameAvailabilityValidatorTest
{
    private readonly Mock<IProductRepository> repository;

    public ProductNameAvailabilityValidatorTest()
    {
        repository = new Mock<IProductRepository>();
    }

    [Fact]
    public async Task IsValidIfNameNotExistsYet()
    {
        repository
            .Setup(l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<short>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(false);
        ProductNameAvailabilityValidator productNameAvailabilityValidator = new(repository.Object);
        await productNameAvailabilityValidator.Validate(
            "Sandwich",
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82")
        );
    }

    [Fact]
    public async Task IsNotValidIfNameAlreadyExists()
    {
        repository
            .Setup(l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<short>(),
                    It.IsAny<string>(),
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(true);
        ProductNameAvailabilityValidator productNameAvailabilityValidator = new(repository.Object);
        await Assert.ThrowsAsync<ProductNameNotAvailableException>(
            () =>
                productNameAvailabilityValidator.Validate(
                    "Sandwich",
                    new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82")
                )
        );
    }
}
