using Microsoft.EntityFrameworkCore;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Paginations;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Infrastructure.Database;
using ProductModel = Src.Core.Shared.Infrastructure.Database.Models.Product;

namespace Src.Core.Products.Infrastructure;

public class PostgresqlProductRepository : IProductRepository
{
    private readonly IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory;

    public PostgresqlProductRepository(
        IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory
    )
    {
        this.databaseContextFactory = databaseContextFactory;
    }

    async public Task<bool> ExistByStatusNotAndNameAndRestaurantId(
        ProductStatus status,
        NonEmptyString name,
        NonNegativeLong restaurantId
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
            throw new DatabaseErrorException(exception.ToString());
        }
    }

    public async Task<Product?> FindByStatusNotAndIdAndRestaurantId(
        ProductStatus status,
        NonNegativeLong id,
        NonNegativeLong restaurantId
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
            return MapToAggregates(productModel);
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }

    private static Product MapToAggregates(ProductModel productModel)
    {
        return new(
            productModel.Id,
            productModel.Name,
            productModel.Price,
            productModel.Description,
            productModel.Status,
            productModel.RestaurantId
        );
    }

    async public Task Save(Product product)
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            ProductModel productModel =
                new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Status = product.Status,
                    RestaurantId = product.RestaurantId
                };
            await databaseContext.Products.AddAsync(productModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
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
            throw new DatabaseErrorException(exception.ToString());
        }
    }

    async public Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        NonNegativeLong restaurantId,
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
            List<Product> products = new();
            foreach (ProductModel productModel in productModels)
            {
                products.Add(MapToAggregates(productModel));
            }
            return products;
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }
}
