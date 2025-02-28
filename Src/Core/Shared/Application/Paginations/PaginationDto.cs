namespace Src.Core.Shared.Application.Paginations;

public record PaginationDto
{
    public short Limit { get; }
    public string? StartIndex { get; }
    public string? SortingField { get; }
    public string? SortingType { get; }

    public PaginationDto(short limit, string? startIndex, string? sortingField, string? sortingType)
    {
        Limit = limit;
        StartIndex = startIndex;
        SortingField = sortingField;
        SortingType = sortingType;
    }
}
