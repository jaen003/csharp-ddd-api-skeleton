using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Src.Api.V1.Schemas;

public class PaginationSchema
{
    [Range(1, int.MaxValue)]
    public int Limit { get; set; }

    [FromQuery(Name = "start_index")]
    public string? StartIndex { get; set; }

    [FromQuery(Name = "sorting_field")]
    public string? SortingField { get; set; }

    [FromQuery(Name = "sorting_type")]
    public string? SortingType { get; set; }
}
