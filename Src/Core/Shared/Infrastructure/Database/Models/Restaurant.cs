using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Src.Core.Shared.Infrastructure.Database.Models;

[Table("restaurant")]
[Index(nameof(Id), nameof(Status))]
public class Restaurant
{
    [Key, Column("id")]
    public long Id { get; set; }

    [Column("name"), MaxLength(40)]
    public string Name { get; set; } = null!;

    [Column("status")]
    public short Status { get; set; }
}
