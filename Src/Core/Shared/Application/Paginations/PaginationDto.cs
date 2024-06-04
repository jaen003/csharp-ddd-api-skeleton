namespace Src.Core.Shared.Application.Paginations;

public class PaginationDto
{
    public int Limit { get; set; }
    public string? StartIndex { get; set; }
    public string? SortingField { get; set; }
    public string? SortingType { get; set; }
}
