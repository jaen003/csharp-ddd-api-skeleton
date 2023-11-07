using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Src.Core.Shared.Infrastructure.Database.Models;

[Table("restaurant")]
[Index(nameof(Id), nameof(Status))]
public class Restaurant
{
    [Key, Column("id"), MaxLength(36), DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string Id { get; set; }

    [Column("name"), MaxLength(40)]
    public string Name { get; set; }

    [Column("status")]
    public short Status { get; set; }

    public Restaurant()
    {
        Name = string.Empty;
        Id = string.Empty;
    }
}
