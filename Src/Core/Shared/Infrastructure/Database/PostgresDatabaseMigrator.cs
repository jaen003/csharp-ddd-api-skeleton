using Microsoft.EntityFrameworkCore;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresDatabaseMigrator
{
    private readonly IDbContextFactory<PostgresDatabaseContext> databaseContextFactory;

    public PostgresDatabaseMigrator(
        IDbContextFactory<PostgresDatabaseContext> databaseContextFactory
    )
    {
        this.databaseContextFactory = databaseContextFactory;
    }

    public async Task Migrate()
    {
        await using PostgresDatabaseContext databaseContext =
            await databaseContextFactory.CreateDbContextAsync();
        await databaseContext.Database.MigrateAsync();
    }
}
