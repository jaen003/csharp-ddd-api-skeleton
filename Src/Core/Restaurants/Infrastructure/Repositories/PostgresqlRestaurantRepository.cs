using Microsoft.EntityFrameworkCore;
using Src.Core.Restaurants.Application;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Restaurants.Infrastructure.Mappers;
using Src.Core.Restaurants.Infrastructure.Models;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.Exceptions;

namespace Src.Core.Restaurants.Infrastructure.Repositories;

public class PostgresqlRestaurantRepository : IRestaurantRepository
{
    private readonly IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory;

    private readonly RestaurantMapper mapper;

    public PostgresqlRestaurantRepository(
        IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory,
        RestaurantMapper mapper
    )
    {
        this.databaseContextFactory = databaseContextFactory;
        this.mapper = mapper;
    }

    public async Task<bool> ExistsByStatusNotAndId(RestaurantStatus status, Uuid id)
    {
        try
        {
            await using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            return await databaseContext.Restaurants.AnyAsync(t =>
                t.Status != status.Value && t.Id == id.Value
            );
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }

    public async Task Save(Restaurant restaurant)
    {
        try
        {
            await using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            RestaurantModel restaurantModel = mapper.ToModel(restaurant);
            await databaseContext.Restaurants.AddAsync(restaurantModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseOperationFailedException(exception.ToString());
        }
    }
}
