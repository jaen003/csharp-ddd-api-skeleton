using System.Collections.ObjectModel;
using Moq;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Exceptions;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Restaurants.Application.Exceptions;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Logging;
using Src.Core.Shared.Domain.Events;

namespace Tests.Products;

public class ProductCreatorTest
{
    private readonly ProductCreationDto creationDto;
    private readonly ILogger logger;
    private readonly Mock<IDomainEventPublisher> eventPublisher;
    private readonly Mock<IProductRepository> repository;
    private readonly Mock<IProductNameAvailabilityValidator> productNameAvailabilityValidator;
    private readonly Mock<IRestaurantExistenceValidator> restaurantExistenceValidator;

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
        eventPublisher = new Mock<IDomainEventPublisher>();
        repository = new Mock<IProductRepository>();
        productNameAvailabilityValidator = new Mock<IProductNameAvailabilityValidator>();
        restaurantExistenceValidator = new Mock<IRestaurantExistenceValidator>();
    }

    [Fact]
    public async Task IsCreatedSuccessfully()
    {
        ProductCreator creator =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                restaurantExistenceValidator.Object,
                productNameAvailabilityValidator.Object
            );
        await creator.Create(creationDto);
        repository.Verify(r => r.Save(It.IsAny<Product>()), Times.Once);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Once
        );
    }

    [Fact]
    public async Task IsNotCreatedIfRestaurantWasNotFound()
    {
        restaurantExistenceValidator
            .Setup(l => l.Validate(It.IsAny<Guid>()))
            .ThrowsAsync(new RestaurantNotFoundException(creationDto.RestaurantId));
        ProductCreator creator =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                restaurantExistenceValidator.Object,
                productNameAvailabilityValidator.Object
            );
        await Assert.ThrowsAsync<RestaurantNotFoundException>(() => creator.Create(creationDto));
        repository.Verify(r => r.Save(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Never
        );
    }

    [Fact]
    public async Task IsNotCreatedIfNameAlreadyExists()
    {
        productNameAvailabilityValidator
            .Setup(l => l.Validate(It.IsAny<string>(), It.IsAny<Guid>()))
            .ThrowsAsync(new ProductNameNotAvailableException(creationDto.Name));
        ProductCreator creator =
            new(
                repository.Object,
                eventPublisher.Object,
                logger,
                restaurantExistenceValidator.Object,
                productNameAvailabilityValidator.Object
            );
        await Assert.ThrowsAsync<ProductNameNotAvailableException>(
            () => creator.Create(creationDto)
        );
        repository.Verify(r => r.Save(It.IsAny<Product>()), Times.Never);
        eventPublisher.Verify(
            r => r.Publish(It.IsAny<ReadOnlyCollection<DomainEvent>>()),
            Times.Never
        );
    }
}
