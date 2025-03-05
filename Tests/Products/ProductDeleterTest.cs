using System.Collections.ObjectModel;
using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Application.UseCases;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Events;

namespace Tests.Products;

public class ProductDeleterTest
{
    private readonly Product product;
    private readonly ProductDeletionData deletionData;
    private readonly ILogger logger;
    private readonly Mock<IDomainEventPublisher> eventPublisher;
    private readonly Mock<IProductRepository> repository;

    public ProductDeleterTest()
    {
        product = new Product(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            1,
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        deletionData = new ProductDeletionData(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = new Mock<IDomainEventPublisher>();
        repository = new Mock<IProductRepository>();
    }

    [Fact]
    public async Task IsDeletedSuccessfully()
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
        ProductDeleter deleter = new(repository.Object, eventPublisher.Object, logger);
        await deleter.Delete(deletionData);
        repository.Verify(r => r.Update(It.Is<Product>(p => p == product)), Times.Once);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Once
        );
    }

    [Fact]
    public async Task IsNotDeletedIfProductWasNotFound()
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
        ProductDeleter deleter = new(repository.Object, eventPublisher.Object, logger);
        await Assert.ThrowsAsync<ProductNotFoundException>(() => deleter.Delete(deletionData));
        repository.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Never
        );
    }
}
