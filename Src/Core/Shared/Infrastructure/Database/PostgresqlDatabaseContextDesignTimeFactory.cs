using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using dotenv.net;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseContextDesignTimeFactory
    : IDesignTimeDbContextFactory<PostgresqlDatabaseContext>
{
    private const string ENVIRONMENT_FILE_NAME = ".env";

    public PostgresqlDatabaseContextDesignTimeFactory()
    {
        LoadEnvironmentFile();
    }

    private static void LoadEnvironmentFile()
    {
        string currentDirectoryPath = Directory.GetCurrentDirectory();
        string projectDirectoryPath = Directory.GetParent(currentDirectoryPath)?.Parent?.FullName!;
        string environmentFilePath = Path.Combine(projectDirectoryPath, ENVIRONMENT_FILE_NAME);
        DotEnv.Fluent().WithEnvFiles(environmentFilePath).Load();
    }

    public PostgresqlDatabaseContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<PostgresqlDatabaseContext> optionsBuilder = new();
        PostgresqlDatabaseConnectionData databaseConnectionData = new();
        optionsBuilder.UseNpgsql(databaseConnectionData.ConnectionString);
        return new PostgresqlDatabaseContext(optionsBuilder.Options);
    }
}
