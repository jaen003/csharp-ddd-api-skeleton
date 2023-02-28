using Microsoft.EntityFrameworkCore;
using Src.Core.Shared.Infrastructure.Database.Models;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseContext : DbContext
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Restaurant> Restaurants { get; set; }

    public PostgresqlDatabaseContext(DbContextOptions<PostgresqlDatabaseContext> options)
        : base(options) { }
}
