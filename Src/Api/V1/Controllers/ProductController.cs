using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Src.Api.V1.InputModels.Paginations;
using Src.Api.V1.InputModels.Products;
using Src.Core.Products.Application;
using Src.Core.Products.Application.Dtos;
using Src.Core.Products.Application.Services;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Paginations;
using ILogger = Src.Core.Shared.Application.Logging.ILogger;

namespace Src.Api.V1.Controllers;

[Route("api/v1/product")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository repository;
    private readonly IDomainEventPublisher eventPublisher;
    private readonly ILogger logger;

    private readonly IRestaurantExistenceValidator restaurantExistenceValidator;
    private readonly IProductNameAvailabilityValidator productNameAvailabilityValidator;

    public ProductController(
        IProductRepository repository,
        IDomainEventPublisher eventPublisher,
        ILogger logger,
        IRestaurantExistenceValidator restaurantExistenceValidator,
        IProductNameAvailabilityValidator productNameAvailabilityValidator
    )
    {
        this.repository = repository;
        this.eventPublisher = eventPublisher;
        this.logger = logger;
        this.restaurantExistenceValidator = restaurantExistenceValidator;
        this.productNameAvailabilityValidator = productNameAvailabilityValidator;
    }

    [HttpPut("create")]
    public async Task Create([FromBody] ProductCreationInputModel inputModel)
    {
        ProductCreator creator =
            new(
                repository,
                eventPublisher,
                logger,
                restaurantExistenceValidator,
                productNameAvailabilityValidator
            );
        ProductCreationDto creationDto =
            new(
                inputModel.Id,
                inputModel.Name,
                inputModel.Price,
                inputModel.Description,
                inputModel.RestaurantId
            );
        await creator.Create(creationDto);
    }

    [HttpGet]
    public async Task<ActionResult<ReadOnlyCollection<ProductDto>>> FindAll(
        [FromQuery] PaginationInputModel paginationInputModel,
        [FromBody] AllProductsQueryInputModel allProductsQueryInputModel
    )
    {
        AllProductsFinder finder = new(repository);
        PaginationDto paginationDto =
            new(
                paginationInputModel.Limit,
                paginationInputModel.StartIndex,
                paginationInputModel.SortingField,
                paginationInputModel.SortingType
            );
        AllProductsQueryDto queryDto = new(paginationDto, allProductsQueryInputModel.RestaurantId);
        return await finder.Find(queryDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> FindById(
        [FromRoute] Guid id,
        [FromBody] ProductByIdQueryInputModel inputModel
    )
    {
        ProductByIdFinder finder = new(repository);
        ProductByIdQueryDto queryDto = new(id, inputModel.RestaurantId);
        return await finder.Find(queryDto);
    }

    [HttpPut("{id}/change/price")]
    public async Task ChangePrice(
        [FromRoute] Guid id,
        [FromBody] ProductPriceChangeInputModel inputModel
    )
    {
        ProductPriceChanger changer = new(repository, eventPublisher, logger);
        ProductPriceChangeDto changeDto = new(id, inputModel.Price, inputModel.RestaurantId);
        await changer.Change(changeDto);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id, [FromBody] ProductDeletionInputModel inputModel)
    {
        ProductDeletor deletor = new(repository, eventPublisher, logger);
        ProductDeletionDto deletionDto = new(id, inputModel.RestaurantId);
        await deletor.Delete(deletionDto);
    }

    [HttpPut("{id}/change/description")]
    public async Task ChangeDescription(
        [FromRoute] Guid id,
        [FromBody] ProductDescriptionChangeInputModel inputModel
    )
    {
        ProductDescriptionChanger changer = new(repository, eventPublisher, logger);
        ProductDescriptionChangeDto changeDto =
            new(id, inputModel.Description, inputModel.RestaurantId);
        await changer.Change(changeDto);
    }

    [HttpPut("{id}/rename")]
    public async Task Rename([FromRoute] Guid id, [FromBody] ProductNameChangeInputModel inputModel)
    {
        ProductRenamer renamer =
            new(repository, eventPublisher, logger, productNameAvailabilityValidator);
        ProductNameChangeDto changeDto = new(id, inputModel.Name, inputModel.RestaurantId);
        await renamer.Rename(changeDto);
    }
}
