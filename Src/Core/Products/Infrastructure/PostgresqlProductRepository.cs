using Microsoft.EntityFrameworkCore;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Paginations;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.Mappers;
using ProductModel = Src.Core.Shared.Infrastructure.Database.Models.Product;

namespace Src.Core.Products.Infrastructure;

public class PostgresqlProductRepository : IProductRepository
{
    private readonly IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory;
    private readonly ProductMapper mapper;

    public PostgresqlProductRepository(
        IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory,
        ProductMapper mapper
    )
    {
        this.databaseContextFactory = databaseContextFactory;
        this.mapper = mapper;
    }

    async public Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        ProductStatus status,
        NonEmptyString name,
        Uuid restaurantId
    )
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            return await databaseContext.Products.AnyAsync(
                t =>
                    t.Status != status.Value
                    && t.Name == name.Value
                    && t.RestaurantId == restaurantId.Value
            );
        }
        catch (Exception exception)
        {
            throw new DatabaseError(exception.ToString());
        }
    }

    public async Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        ProductStatus status,
        Uuid id,
        Uuid restaurantId
    )
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel? productModel = await databaseContext.Products.FirstOrDefaultAsync(
                t =>
                    t.Status != status.Value
                    && t.Id == id.Value
                    && t.RestaurantId == restaurantId.Value
            );
            if (productModel == null)
            {
                return null;
            }
            return mapper.ToEntity(productModel);
        }
        catch (Exception exception)
        {
            throw new DatabaseError(exception.ToString());
        }
    }

    async public Task Save(Product product)
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel productModel = mapper.ToModel(product);
            await databaseContext.Products.AddAsync(productModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseError(exception.ToString());
        }
    }

    async public Task Update(Product product)
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel productModel = await databaseContext.Products.FirstAsync(
                t => t.Id == product.Id
            );
            productModel.Name = product.Name;
            productModel.Price = product.Price;
            productModel.Description = product.Description;
            productModel.Status = product.Status;
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseError(exception.ToString());
        }
    }

    async public Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        Uuid restaurantId,
        Pagination pagination
    )
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            List<ProductModel> productModels = await databaseContext.Products
                .Where(t => t.Status != status.Value && t.RestaurantId == restaurantId.Value)
                .AddPagination(pagination)
                .ToListAsync();
            return mapper.ToEntities(productModels);
        }
        catch (Exception exception)
        {
            throw new DatabaseError(exception.ToString());
        }
    }
}
