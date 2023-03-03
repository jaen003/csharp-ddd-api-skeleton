using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Src.Api.V1.Schemas.Products;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Generators;
using ILogger = Src.Core.Shared.Domain.Logging.ILogger;

namespace Src.Api.V1.Controllers;

[Route("api/v1/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository repository;
    private readonly IRestaurantRepository restaurantRepository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;
    private readonly IIdentifierGenerator identifierGenerator;

    public ProductController(
        IProductRepository repository,
        IRestaurantRepository restaurantRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        IIdentifierGenerator identifierGenerator
    )
    {
        this.repository = repository;
        this.restaurantRepository = restaurantRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.identifierGenerator = identifierGenerator;
    }

    [HttpPut("create")]
    public async Task Create(
        ProductCreationSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] long? restaurantId
    )
    {
        ProductCreator creator =
            new(repository, restaurantRepository, eventPublisher, logger, identifierGenerator);
        await creator.Create(
            new ProductName(schema.Name),
            new ProductPrice(schema.Price),
            new ProductDescription(schema.Description),
            new RestaurantId(restaurantId ?? 0)
        );
    }
}
