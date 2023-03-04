using Moq;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Logging;

namespace Tests.Products;

public class ProductPriceChangerTest
{
    private readonly Product product;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductPriceChangerTest()
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
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsChangedSuccessfully()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<ProductId>(),
                    It.IsAny<RestaurantId>()
                ) == Task.FromResult(product)
        );
        int exceptionCode = 0;
        try
        {
            ProductPriceChanger changer = new(repository, eventPublisher, logger);
            await changer.Change(product.Id, product.Price, product.RestaurantId);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotChangedIfProductWasNotFound()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<ProductId>(),
                    It.IsAny<RestaurantId>()
                ) == Task.FromResult<Product>(null!)
        );
        int exceptionCode = 0;
        try
        {
            ProductPriceChanger changer = new(repository, eventPublisher, logger);
            await changer.Change(product.Id, product.Price, product.RestaurantId);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(202, exceptionCode);
    }
}
