using Microsoft.EntityFrameworkCore;
using Src.Core.Products.Application;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Infrastructure.Mappers;
using Src.Core.Products.Infrastructure.Models;
using Src.Core.Shared.Domain.Paginations.Aggregates;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.Exceptions;

namespace Src.Core.Products.Infrastructure;

public class PostgresProductRepository : IProductRepository
{
    private readonly IDbContextFactory<PostgresDatabaseContext> databaseContextFactory;
    private readonly ProductMapper mapper;

    public PostgresProductRepository(
        IDbContextFactory<PostgresDatabaseContext> databaseContextFactory,
        ProductMapper mapper
    )
    {
        this.databaseContextFactory = databaseContextFactory;
        this.mapper = mapper;
    }

    public async Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        short status,
        string name,
        Guid restaurantId
    )
    {
        try
        {
            await using PostgresDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            return await databaseContext.Products.AnyAsync(t =>
                t.Status != status && t.Name == name && t.RestaurantId == restaurantId
            );
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }

    public async Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        short status,
        Guid id,
        Guid restaurantId
    )
    {
        try
        {
            await using PostgresDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel? productModel = await databaseContext.Products.FirstOrDefaultAsync(t =>
                t.Status != status && t.Id == id && t.RestaurantId == restaurantId
            );
            if (productModel == null)
            {
                return null;
            }
            return mapper.ToEntity(productModel);
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }

    public async Task Save(Product product)
    {
        try
        {
            await using PostgresDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel productModel = mapper.ToModel(product);
            await databaseContext.Products.AddAsync(productModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }

    public async Task Update(Product product)
    {
        try
        {
            await using PostgresDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel productModel = await databaseContext.Products.FirstAsync(t =>
                t.Id == product.Id
            );
            productModel.Name = product.Name;
            productModel.Price = product.Price;
            productModel.Description = product.Description;
            productModel.Status = product.Status;
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }

    public async Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        short status,
        Guid restaurantId,
        Pagination pagination
    )
    {
        try
        {
            await using PostgresDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            List<ProductModel> productModels = await databaseContext
                .Products.Where(t => t.Status != status && t.RestaurantId == restaurantId)
                .AddPagination(pagination)
                .AsNoTracking()
                .ToListAsync();
            return mapper.ToEntities(productModels);
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }
}
