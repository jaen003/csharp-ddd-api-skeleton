using Microsoft.EntityFrameworkCore;
using Src.Core.Products.Domain;
using Src.Core.Products.Domain.Aggregates;
using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Paginations;
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
        ProductName name,
        RestaurantId restaurantId
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
        ProductId id,
        RestaurantId restaurantId
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
            new ProductId(productModel.Id),
            new ProductName(productModel.Name),
            new ProductPrice(productModel.Price),
            new ProductDescription(productModel.Description),
            new ProductStatus(productModel.Status),
            new RestaurantId(productModel.RestaurantId)
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
                    Id = product.Id.Value,
                    Name = product.Name.Value,
                    Price = product.Price.Value,
                    Description = product.Description.Value,
                    Status = product.Status.Value,
                    RestaurantId = product.RestaurantId.Value
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
                t => t.Id == product.Id.Value
            );
            productModel.Name = product.Name.Value;
            productModel.Price = product.Price.Value;
            productModel.Description = product.Description.Value;
            productModel.Status = product.Status.Value;
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }

    async public Task<List<Product>> FindByStatusNotAndRestaurantIdAndPagination(
        ProductStatus status,
        RestaurantId restaurantId,
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
