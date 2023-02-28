using Microsoft.EntityFrameworkCore;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseMigrator
{
    private readonly IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory;

    public PostgresqlDatabaseMigrator(
        IDbContextFactory<PostgresqlDatabaseContext> databaseContextFactory
    )
    {
        this.databaseContextFactory = databaseContextFactory;
    }

    public async Task Migrate()
    {
        using PostgresqlDatabaseContext databaseContext =
            await databaseContextFactory.CreateDbContextAsync();
        await databaseContext.Database.MigrateAsync();
    }
}
