using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Src.Api.V1.Schemas;
using Src.Api.V1.Schemas.Products;
using Src.Core.Products.Application.Services;
using Src.Core.Products.Domain;
using Src.Core.Restaurants.Domain;
using Src.Core.Shared.Domain.EventBus;
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

    public ProductController(
        IProductRepository repository,
        IRestaurantRepository restaurantRepository,
        IDomainEventPublisher eventPublisher,
        ILogger logger
    )
    {
        this.repository = repository;
        this.restaurantRepository = restaurantRepository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
    }

    [HttpPut("create")]
    public async Task Create(
        ProductCreationSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductCreator creator = new(repository, restaurantRepository, eventPublisher, logger);
        await creator.Create(
            schema.Id,
            schema.Name,
            schema.Price,
            schema.Description,
            restaurantId!
        );
    }

    [HttpGet]
    public async Task<ActionResult<List<Dictionary<string, object>>>> FindAll(
        [FromQuery] PaginationSchema paginationSchema,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        AllProductsFinder finder = new(repository);
        Pagination pagination = Pagination.FromPrimitives(
            paginationSchema.Limit,
            paginationSchema.StartIndex,
            paginationSchema.SortingField,
            paginationSchema.SortingType
        );
        return await finder.Find(restaurantId!, pagination);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Dictionary<string, object>>> FindById(
        [FromRoute] string id,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductByIdFinder finder = new(repository);
        return await finder.Find(id, restaurantId!);
    }

    [HttpPut("{id}/change/price")]
    public async Task ChangePrice(
        [FromRoute] string id,
        ProductPriceChangerSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductPriceChanger changer = new(repository, eventPublisher, logger);
        await changer.Change(id, schema.Price, restaurantId!);
    }

    [HttpDelete("{id}")]
    public async Task Delete(
        [FromRoute] string id,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductDeletor deletor = new(repository, eventPublisher, logger);
        await deletor.Delete(id, restaurantId!);
    }

    [HttpPut("{id}/change/description")]
    public async Task ChangeDescription(
        [FromRoute] string id,
        ProductDescriptionChangeSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductDescriptionChanger changer = new(repository, eventPublisher, logger);
        await changer.Change(id, schema.Description, restaurantId!);
    }

    [HttpPut("{id}/rename")]
    public async Task Rename(
        string id,
        ProductNameChangeSchema schema,
        [Required, FromHeader(Name = "restaurant_id")] string? restaurantId
    )
    {
        ProductRenamer renamer = new(repository, eventPublisher, logger);
        await renamer.Rename(id, schema.Name, restaurantId!);
    }
}
