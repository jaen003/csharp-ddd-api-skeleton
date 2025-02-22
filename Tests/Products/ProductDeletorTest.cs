using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Exceptions;

namespace Tests.Products;

public class ProductDeletorTest
{
    private readonly Product product;
    private readonly ProductDeletionDto deletionDto;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductDeletorTest()
    {
        product = new Product(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            1,
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        deletionDto = new ProductDeletionDto(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsDeletedSuccessfully()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(l =>
            l.FindByStatusNotAndIdAndRestaurantId(
                It.IsAny<ProductStatus>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>()
            ) == Task.FromResult(product)
        );
        int exceptionCode = 0;
        try
        {
            ProductDeletor deletor = new(repository, eventPublisher, logger);
            await deletor.Delete(deletionDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotDeletedIfProductWasNotFound()
    {
        IProductRepository repository = Mock.Of<IProductRepository>(l =>
            l.FindByStatusNotAndIdAndRestaurantId(
                It.IsAny<ProductStatus>(),
                It.IsAny<Guid>(),
                It.IsAny<Guid>()
            ) == Task.FromResult<Product>(null!)
        );
        int exceptionCode = 0;
        try
        {
            ProductDeletor deletor = new(repository, eventPublisher, logger);
            await deletor.Delete(deletionDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(202, exceptionCode);
    }
}
