using Microsoft.EntityFrameworkCore;
using Src.Core.Restaurants.Domain;
using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;
using Src.Core.Shared.Infrastructure.Database;
using Src.Core.Shared.Infrastructure.Mappers;
using RestaurantModel = Src.Core.Shared.Infrastructure.Database.Models.Restaurant;

namespace Src.Core.Restaurants.Infrastructure;

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

    async public Task<bool> ExistsByStatusNotAndId(RestaurantStatus status, NonNegativeLong id)
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
            RestaurantModel restaurantModel = mapper.ToModel(restaurant);
            await databaseContext.Restaurants.AddAsync(restaurantModel);
            await databaseContext.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            throw new DatabaseErrorException(exception.ToString());
        }
    }
}
