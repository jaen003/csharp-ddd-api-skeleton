namespace Src.Core.Shared.Application.Paginations;

public record PaginationDto
{
    public int Limit { get; }
    public string? StartIndex { get; }
    public string? SortingField { get; }
    public string? SortingType { get; }

    public PaginationDto(int limit, string? startIndex, string? sortingField, string? sortingType)
    {
        Limit = limit;
        StartIndex = startIndex;
        SortingField = sortingField;
        SortingType = sortingType;
    }
}
