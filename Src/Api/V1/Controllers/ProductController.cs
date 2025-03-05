using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Src.Api.V1.InputModels.Paginations;
using Src.Api.V1.InputModels.Products;
using Src.Core.Products.Application;
using Src.Core.Products.Application.DTOs;
using Src.Core.Products.Application.UseCases;
using Src.Core.Products.Application.Validators;
using Src.Core.Restaurants.Application.Validators;
using Src.Core.Shared.Application.EventBus;
using Src.Core.Shared.Application.Paginations.DTOs;
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
        ProductCreationData creationData =
            new(
                inputModel.Id,
                inputModel.Name,
                inputModel.Price,
                inputModel.Description,
                inputModel.RestaurantId
            );
        await creator.Create(creationData);
    }

    [HttpGet]
    public async Task<ActionResult<ReadOnlyCollection<ProductResponseData>>> SearchAll(
        [FromQuery] PaginationInputModel paginationInputModel,
        [FromBody] AllProductsQueryInputModel allProductsQueryInputModel
    )
    {
        AllProductsSearcher searcher = new(repository);
        PaginationData paginationData =
            new(
                paginationInputModel.Limit,
                paginationInputModel.StartIndex,
                paginationInputModel.SortingField,
                paginationInputModel.SortingType
            );
        AllProductsSearchData searchData =
            new(paginationData, allProductsQueryInputModel.RestaurantId);
        return await searcher.Search(searchData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseData>> SearchById(
        [FromRoute] Guid id,
        [FromBody] ProductByIdQueryInputModel inputModel
    )
    {
        ProductByIdSearcher searcher = new(repository);
        ProductSearchData searchData = new(id, inputModel.RestaurantId);
        return await searcher.Search(searchData);
    }

    [HttpPut("{id}/change/price")]
    public async Task ChangePrice(
        [FromRoute] Guid id,
        [FromBody] ProductPriceChangeInputModel inputModel
    )
    {
        ProductPriceChanger changer = new(repository, eventPublisher, logger);
        ProductPriceChangeData changeData = new(id, inputModel.Price, inputModel.RestaurantId);
        await changer.Change(changeData);
    }

    [HttpDelete("{id}")]
    public async Task Delete([FromRoute] Guid id, [FromBody] ProductDeletionInputModel inputModel)
    {
        ProductDeleter deleter = new(repository, eventPublisher, logger);
        ProductDeletionData deletionData = new(id, inputModel.RestaurantId);
        await deleter.Delete(deletionData);
    }

    [HttpPut("{id}/change/description")]
    public async Task ChangeDescription(
        [FromRoute] Guid id,
        [FromBody] ProductDescriptionChangeInputModel inputModel
    )
    {
        ProductDescriptionChanger changer = new(repository, eventPublisher, logger);
        ProductDescriptionChangeData changeData =
            new(id, inputModel.Description, inputModel.RestaurantId);
        await changer.Change(changeData);
    }

    [HttpPut("{id}/rename")]
    public async Task Rename([FromRoute] Guid id, [FromBody] ProductNameChangeInputModel inputModel)
    {
        ProductRenamer renamer =
            new(repository, eventPublisher, logger, productNameAvailabilityValidator);
        ProductNameChangeData changeData = new(id, inputModel.Name, inputModel.RestaurantId);
        await renamer.Rename(changeData);
    }
}
