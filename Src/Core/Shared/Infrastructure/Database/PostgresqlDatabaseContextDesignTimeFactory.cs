using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseContextDesignTimeFactory
    : IDesignTimeDbContextFactory<PostgresqlDatabaseContext>
{
    public PostgresqlDatabaseContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PostgresqlDatabaseContext> optionsBuilder = new();
        PostgresqlDatabaseConnectionData databaseConnectionData = new();
        optionsBuilder.UseNpgsql(databaseConnectionData.ConnectionString);
        return new PostgresqlDatabaseContext(optionsBuilder.Options);
    }
}
