using Moq;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.Logging;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Products;

public class ProductCreatorTest
{
    private readonly Product product;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductCreatorTest()
    {
        product = new Product(
            "a1433e47-9708-4e61-adfc-6de2ad462f82",
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            1,
            "82022d1f-b0fa-4b70-86ae-e99c3101fb47"
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsCreatedSuccessfully()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(
            l =>
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<Uuid>())
                == Task.FromResult(true)
        );
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<NonEmptyString>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult(false)
        );
        int exceptionCode = 0;
        try
        {
            ProductCreator creator = new(repository, restaurantRepository, eventPublisher, logger);
            await creator.Create(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotCreatedIfRestaurantWasNotFound()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(
            l =>
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<Uuid>())
                == Task.FromResult(false)
        );
        IProductRepository repository = Mock.Of<IProductRepository>();
        int exceptionCode = 0;
        try
        {
            ProductCreator creator = new(repository, restaurantRepository, eventPublisher, logger);
            await creator.Create(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(101, exceptionCode);
    }

    [Fact]
    public async Task IsNotCreatedIfNameAlreadyExists()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(
            l =>
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<Uuid>())
                == Task.FromResult(true)
        );
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<NonEmptyString>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult(true)
        );
        int exceptionCode = 0;
        try
        {
            ProductCreator creator = new(repository, restaurantRepository, eventPublisher, logger);
            await creator.Create(
                product.Id,
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(201, exceptionCode);
    }
}
