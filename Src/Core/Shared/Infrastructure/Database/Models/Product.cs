using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Src.Core.Shared.Infrastructure.Database.Models;

[Table("product")]
[Index(nameof(Name), nameof(Status), nameof(RestaurantId))]
public class Product
{
    [Key, Column("id")]
    public long Id { get; set; }

    [Column("name"), MaxLength(60)]
    public required string Name { get; set; }

    [Column("price")]
    public int Price { get; set; }

    [Column("description"), MaxLength(80)]
    public required string Description { get; set; }

    [Column("status")]
    public short Status { get; set; }

    [Column("restaurant_id")]
    public long RestaurantId { get; set; }

    private Restaurant? restaurant;

    public Restaurant Restaurant
    {
        get => lazyLoader.Load(this, ref restaurant)!;
        set => restaurant = value;
    }
    private readonly ILazyLoader lazyLoader = null!;

    public Product(ILazyLoader lazyLoader)
    {
        this.lazyLoader = lazyLoader;
    }

    public Product() { }
}
