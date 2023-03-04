using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Src.Api.V1.Schemas;
using Src.Api.V1.Schemas.Products;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.EventBus;
using Src.Core.Shared.Domain.Generators;
using Src.Core.Shared.Domain.Paginations;
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

    [HttpGet]
    public async Task<ActionResult<List<Dictionary<string, object>>>> FindByRestaurant(
        [FromQuery] PaginationSchema paginationSchema,
        [Required, FromHeader(Name = "restaurant_id")] long? restaurantId
    )
    {
        ProductFinder finder = new(repository);
        Pagination pagination = Pagination.FromPrimitives(
            paginationSchema.Limit,
            paginationSchema.StartIndex,
            paginationSchema.SortingField,
            paginationSchema.SortingType
        );
        return await finder.FindByResturantIdAndPagination(
            new RestaurantId(restaurantId ?? 0),
            pagination
        );
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<Dictionary<string, object>>> FindById(
        long id,
        [Required, FromHeader(Name = "restaurant_id")] long? restaurantId
    )
    {
        ProductFinder finder = new(repository);
        return await finder.FindByIdAndResturantId(
            new ProductId(id),
            new RestaurantId(restaurantId ?? 0)
        );
    }

    [HttpPut("{id:long}/change/price")]
    public async Task ChangePrice(
        long id,
        ProductPriceChangerSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] long? restaurantId
    )
    {
        ProductPriceChanger changer = new(repository, eventPublisher, logger);
        await changer.Change(
            new ProductId(id),
            new ProductPrice(schema.Price),
            new RestaurantId(restaurantId ?? 0)
        );
    }

    [HttpDelete("{id:long}")]
    public async Task Delete(
        long id,
        [Required, FromHeader(Name = "restaurant_id")] long? restaurantId
    )
    {
        ProductDeletor deletor = new(repository, eventPublisher, logger);
        await deletor.Delete(new ProductId(id), new RestaurantId(restaurantId ?? 0));
    }
}
