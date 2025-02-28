using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Events;

namespace Tests.Products;

public class ProductDescriptionChangerTest
{
    private readonly Product product;
    private readonly ProductDescriptionChangeDto changeDto;
    private readonly ILogger logger;
    private readonly Mock<IDomainEventPublisher> eventPublisher;
    private readonly Mock<IProductRepository> repository;

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
        eventPublisher = new Mock<IDomainEventPublisher>();
        repository = new Mock<IProductRepository>();
    }

    [Fact]
    public async Task IsChangedSuccessfully()
    {
        repository
            .Setup(l =>
                l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<short>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(product);
        ProductDescriptionChanger changer = new(repository.Object, eventPublisher.Object, logger);
        await changer.Change(changeDto);
        repository.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        eventPublisher.Verify(r => r.Publish(It.IsAny<List<DomainEvent>>()), Times.Once);
    }

    [Fact]
    public async Task IsNotChangedIfProductWasNotFound()
    {
        repository
            .Setup(l =>
                l.FindByStatusNotAndIdAndRestaurantId(
                    It.IsAny<short>(),
                    It.IsAny<Guid>(),
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(null as Product);
        ProductDescriptionChanger changer = new(repository.Object, eventPublisher.Object, logger);
        await Assert.ThrowsAsync<ProductNotFoundException>(() => changer.Change(changeDto));
        repository.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(r => r.Publish(It.IsAny<List<DomainEvent>>()), Times.Never);
    }
}
