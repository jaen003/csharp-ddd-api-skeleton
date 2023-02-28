namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseConnectionData
{
    public int PoolSize { get; }
    public string ConnectionString { get; }

    public PostgresqlDatabaseConnectionData()
    {
        PoolSize = int.Parse(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_POOL_SIZE")!);
        ConnectionString = GenerateConnectionString();
    }

    private static string GenerateConnectionString()
    {
        string password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD")!;
        string user = Environment.GetEnvironmentVariable("DATABASE_USER")!;
        string database = Environment.GetEnvironmentVariable("DATABASE_NAME")!;
        string host = Environment.GetEnvironmentVariable("DATABASE_HOST")!;
        int port = int.Parse(Environment.GetEnvironmentVariable("DATABASE_PORT")!);
        return $"Host={host};Port={port};Database={database};Username={user};Password={password};";
    }
}
