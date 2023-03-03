using Moq;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Domain.Logging;

namespace Tests.Products;

public class ProductCreatorTest
{
    private readonly Product product;
    private readonly ILogger logger;
    private readonly IIdentifierGenerator identifierGenerator;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductCreatorTest()
    {
        product = new Product(
            new ProductId(1),
            new ProductName("Sandwich"),
            new ProductPrice(3),
            new ProductDescription("Bread, Onion, Tomato, Chicken"),
            ProductStatus.CreateActived(),
            new RestaurantId(1)
        );
        logger = Mock.Of<ILogger>();
        identifierGenerator = Mock.Of<IIdentifierGenerator>();
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsCreatedSuccessfully()
    {
        IRestaurantRepository restaurantRepository = Mock.Of<IRestaurantRepository>(
            l =>
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<RestaurantId>())
                == Task.FromResult(true)
        );
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<ProductName>(),
                    It.IsAny<RestaurantId>()
                ) == Task.FromResult(false)
        );
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(repository, restaurantRepository, eventPublisher, logger, identifierGenerator);
            await creator.Create(
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (DomainException exception)
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
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<RestaurantId>())
                == Task.FromResult(false)
        );
        IProductRepository repository = Mock.Of<IProductRepository>();
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(repository, restaurantRepository, eventPublisher, logger, identifierGenerator);
            await creator.Create(
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (DomainException exception)
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
                l.ExistsByStatusNotAndId(It.IsAny<RestaurantStatus>(), It.IsAny<RestaurantId>())
                == Task.FromResult(true)
        );
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<ProductName>(),
                    It.IsAny<RestaurantId>()
                ) == Task.FromResult(true)
        );
        int exceptionCode = 0;
        try
        {
            ProductCreator creator =
                new(repository, restaurantRepository, eventPublisher, logger, identifierGenerator);
            await creator.Create(
                product.Name,
                product.Price,
                product.Description,
                product.RestaurantId
            );
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(201, exceptionCode);
    }
}
