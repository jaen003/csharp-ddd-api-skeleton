using Moq;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.Logging;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Products;

public class ProductRenamerTest
{
    private readonly Product product;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductRenamerTest()
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
    public async Task IsRenamedSuccessfully()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<NonEmptyString>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult(false)
                && l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<Uuid>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult(product)
        );
        int exceptionCode = 0;
        try
        {
            ProductRenamer renamer = new(repository, eventPublisher, logger);
            await renamer.Rename(product.Id, product.Name, product.RestaurantId);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotRenamedIfNameAlreadyExists()
    {
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
            ProductRenamer renamer = new(repository, eventPublisher, logger);
            await renamer.Rename(product.Id, product.Name, product.RestaurantId);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(201, exceptionCode);
    }

    [Fact]
    public async Task IsNotRenamedIfProductWasNotFound()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(
            l =>
                l.ExistByStatusNotAndNameAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<NonEmptyString>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult(false)
                && l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<ProductStatus>(),
                    It.IsAny<Uuid>(),
                    It.IsAny<Uuid>()
                ) == Task.FromResult<Product>(null!)
        );
        int exceptionCode = 0;
        try
        {
            ProductRenamer renamer = new(repository, eventPublisher, logger);
            await renamer.Rename(product.Id, product.Name, product.RestaurantId);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(202, exceptionCode);
    }
}
