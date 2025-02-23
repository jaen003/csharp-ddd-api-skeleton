using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Products;

public class ProductCreatorTest
{
    private readonly ProductCreationDto creationDto;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductCreatorTest()
    {
        creationDto = new ProductCreationDto(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsCreatedSuccessfully()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(l =>
            l.ExistsByStatusNotAndId(It.IsAny<short>(), It.IsAny<Guid>()) == Task.FromResult(true)
        );
        RestaurantExistenceValidator restaurantExistenceValidator = new(restaurantRepository);
        IProductRepository repository = Mock.Of<IProductRepository>(l =>
            l.ExistByStatusNotAndNameAndRestaurantId(
                It.IsAny<short>(),
                It.IsAny<string>(),
                It.IsAny<Guid>()
            ) == Task.FromResult(false)
        );
        ProductNameAvailabilityValidator productNameAvailabilityValidator = new(repository);
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(
                    repository,
                    eventPublisher,
                    logger,
                    restaurantExistenceValidator,
                    productNameAvailabilityValidator
                );
            await creator.Create(creationDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotCreatedIfRestaurantWasNotFound()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(l =>
            l.ExistsByStatusNotAndId(It.IsAny<short>(), It.IsAny<Guid>()) == Task.FromResult(false)
        );
        RestaurantExistenceValidator restaurantExistenceValidator = new(restaurantRepository);
        IProductRepository repository = Mock.Of<IProductRepository>();
        ProductNameAvailabilityValidator productNameAvailabilityValidator = new(repository);
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(
                    repository,
                    eventPublisher,
                    logger,
                    restaurantExistenceValidator,
                    productNameAvailabilityValidator
                );
            await creator.Create(creationDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(101, exceptionCode);
    }

    [Fact]
    public async Task IsNotCreatedIfNameAlreadyExists()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(l =>
            l.ExistsByStatusNotAndId(It.IsAny<short>(), It.IsAny<Guid>()) == Task.FromResult(true)
        );
        RestaurantExistenceValidator restaurantExistenceValidator = new(restaurantRepository);
        IProductRepository repository = Mock.Of<IProductRepository>(l =>
            l.ExistByStatusNotAndNameAndRestaurantId(
                It.IsAny<short>(),
                It.IsAny<string>(),
                It.IsAny<Guid>()
            ) == Task.FromResult(true)
        );
        ProductNameAvailabilityValidator productNameAvailabilityValidator = new(repository);
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(
                    repository,
                    eventPublisher,
                    logger,
                    restaurantExistenceValidator,
                    productNameAvailabilityValidator
                );
            await creator.Create(creationDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(201, exceptionCode);
    }
}
