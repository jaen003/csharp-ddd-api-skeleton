using Microsoft.EntityFrameworkCore;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Infrastructure.Database;
using RestaurantModel = Src.Core.Shared.Infrastructure.Database.Models.Restaurant;

namespace Src.Core.Restaurants.Infrastructure;

public class PostgresqlRestaurantRepository : IRestaurantRepository
{
    private readonly IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory;

    public PostgresqlRestaurantRepository(
        IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory
    )
    {
        this.databaseContextFactory = databaseContextFactory;
    }

    async public Task<bool> ExistsByStatusNotAndId(
        RestaurantStatus status,
        NonNegativeLongValueObject id
    )
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            return await databaseContext.Restaurants.AnyAsync(
                t => t.Status != status.Value && t.Id == id.Value
            );
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }

    async public Task Save(Restaurant restaurant)
    {
        try
        {
            using PostgresqlDatabaseContext databaseContext =
                await databaseContextFactory.CreateDbContextAsync();
            RestaurantModel restaurantModel =
                new()
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name,
                    Status = restaurant.Status
                };
            await databaseContext.Restaurants.AddAsync(restaurantModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }
}
