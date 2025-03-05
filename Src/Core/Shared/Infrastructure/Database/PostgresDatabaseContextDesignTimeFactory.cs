using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresDatabaseContextDesignTimeFactory
    : IDesignTimeDbContextFactory<PostgresDatabaseContext>
{
    public PostgresDatabaseContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PostgresDatabaseContext> optionsBuilder = new();
        PostgresDatabaseConnectionData databaseConnectionData = new();
        optionsBuilder.UseNpgsql(databaseConnectionData.ConnectionString);
        return new PostgresDatabaseContext(optionsBuilder.Options);
    }
}
