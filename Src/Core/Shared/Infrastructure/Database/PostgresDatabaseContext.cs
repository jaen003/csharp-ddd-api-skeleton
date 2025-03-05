using Microsoft.EntityFrameworkCore;
using Src.Core.Products.Infrastructure.Models;
using Src.Core.Restaurants.Infrastructure.Models;

namespace Src.Core.Shared.Infrastructure.Database;

public class PostgresDatabaseContext : DbContext
{
    public DbSet<ProductModel> Products { get; set; } = null!;
    public DbSet<RestaurantModel> Restaurants { get; set; } = null!;

    public PostgresDatabaseContext(DbContextOptions<PostgresDatabaseContext> options)
        : base(options) { }
}
