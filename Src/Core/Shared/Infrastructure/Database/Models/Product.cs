using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Src.Core.Shared.Infrastructure.Database.Models;

[Table("product")]
[Index(nameof(Name), nameof(Status), nameof(RestaurantId))]
public class Product
{
    [Key, Column("id"), MaxLength(36), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }

    [Column("name"), MaxLength(60)]
    public string Name { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [Column("description"), MaxLength(80)]
    public string Description { get; set; }

    [Column("status")]
    public short Status { get; set; }

    [Column("restaurant_id"), MaxLength(36)]
    public string RestaurantId { get; set; }

    private Restaurant? restaurant;

    private readonly ILazyLoader lazyLoader = null!;

    public Restaurant Restaurant
    {
        get => lazyLoader.Load(this, ref restaurant)!;
        set => restaurant = value;
    }

    public Product(ILazyLoader lazyLoader)
    {
        this.lazyLoader = lazyLoader;
        Id = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        RestaurantId = string.Empty;
    }

    public Product()
    {
        Id = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        RestaurantId = string.Empty;
    }
}
