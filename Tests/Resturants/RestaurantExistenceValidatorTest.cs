using Moq;
using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Application.Exceptions;
using Src.Core.Restaurants.Application.Validators;

namespace Tests.Resturants;

public class RestaurantExistenceValidatorTest
{
    private readonly Mock<IRestaurantRepository> repository;

    public RestaurantExistenceValidatorTest()
    {
        repository = new Mock<IRestaurantRepository>();
    }

    [Fact]
    public async Task IsValidIfRestaurantWasFound()
    {
        repository
            .Setup(l => l.ExistsByStatusNotAndId(It.IsAny<short>(), It.IsAny<Guid>()))
            .ReturnsAsync(true);
        RestaurantExistenceValidator restaurantExistenceValidator = new(repository.Object);
        await restaurantExistenceValidator.Validate(
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
    }

    [Fact]
    public async Task IsNotValidIfRestaurantWasNotFound()
    {
        repository
            .Setup(l => l.ExistsByStatusNotAndId(It.IsAny<short>(), It.IsAny<Guid>()))
            .ReturnsAsync(false);
        RestaurantExistenceValidator restaurantExistenceValidator = new(repository.Object);
        await Assert.ThrowsAsync<RestaurantNotFoundException>(
            () =>
                restaurantExistenceValidator.Validate(
                    new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
                )
        );
    }
}
