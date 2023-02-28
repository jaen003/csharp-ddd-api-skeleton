using Microsoft.EntityFrameworkCore;
using Src.Core.Shared.Infrastructure.Database.Models;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresqlDatabaseContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Restaurant> Restaurants { get; set; } = null!;

    public PostgresqlDatabaseContext(DbContextOptions<PostgresqlDatabaseContext> options)
        : base(options) { }
}
