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

public class ProductDescriptionChangerTest
{
    private readonly Product product;
    private readonly ProductDescriptionChangeDto changeDto;
    private readonly ILogger logger;
    private readonly IDomainEventPublisher eventPublisher;

    public ProductDescriptionChangerTest()
    {
        product = new Product(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            1,
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        changeDto = new ProductDescriptionChangeDto(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Bread, Onion, Tomato, Chicken",
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = Mock.Of<IDomainEventPublisher>();
    }

    [Fact]
    public async Task IsChangedSuccessfully()
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
            ProductDescriptionChanger changer = new(repository, eventPublisher, logger);
            await changer.Change(changeDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public async Task IsNotChangedIfProductWasNotFound()
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
            ProductDescriptionChanger changer = new(repository, eventPublisher, logger);
            await changer.Change(changeDto);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(202, exceptionCode);
    }
}
