using System.Collections.ObjectModel;
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

public class ProductRenamerTest
{
    private readonly Product product;
    private readonly ProductNameChangeDto changeDto;
    private readonly ILogger logger;
    private readonly Mock<IDomainEventPublisher> eventPublisher;
    private readonly Mock<IProductRepository> repository;
    private readonly Mock<IProductNameAvailabilityValidator> productNameAvailabilityValidator;

    public ProductRenamerTest()
    {
        product = new Product(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            3,
            "Bread, Onion, Tomato, Chicken",
            1,
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        changeDto = new ProductNameChangeDto(
            new Guid("a1433e47-9708-4e61-adfc-6de2ad462f82"),
            "Sandwich",
            new Guid("82022d1f-b0fa-4b70-86ae-e99c3101fb47")
        );
        logger = Mock.Of<ILogger>();
        eventPublisher = new Mock<IDomainEventPublisher>();
        repository = new Mock<IProductRepository>();
        productNameAvailabilityValidator = new Mock<IProductNameAvailabilityValidator>();
    }

    [Fact]
    public async Task IsRenamedSuccessfully()
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
        ProductRenamer renamer =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                productNameAvailabilityValidator.Object
            );
        await renamer.Rename(changeDto);
        repository.Verify(r => r.Update(It.IsAny<Product>()), Times.Once);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Once
        );
    }

    [Fact]
    public async Task IsNotRenamedIfNameAlreadyExists()
    {
        productNameAvailabilityValidator
            .Setup(l => l.Validate(It.IsAny<string>(), It.IsAny<Guid>()))
            .ThrowsAsync(new ProductNameNotAvailableException(changeDto.Name));
        ProductRenamer renamer =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                productNameAvailabilityValidator.Object
            );
        await Assert.ThrowsAsync<ProductNameNotAvailableException>(() => renamer.Rename(changeDto));
        repository.Verify(r => r.Save(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Never
        );
    }

    [Fact]
    public async Task IsNotRenamedIfProductWasNotFound()
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
        ProductRenamer renamer =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                productNameAvailabilityValidator.Object
            );
        await Assert.ThrowsAsync<ProductNotFoundException>(() => renamer.Rename(changeDto));
        repository.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Never
        );
    }
}
