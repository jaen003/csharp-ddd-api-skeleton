namespace Src.Core.Shared.Application.Paginations.Dtos;

public record PaginationData
{
    public short Limit { get; }
    public string? StartIndex { get; }
    public string? SortingField { get; }
    public string? SortingType { get; }

    public PaginationData(
        short limit,
        string? startIndex,
        string? sortingField,
        string? sortingType
    )
    {
        Limit = limit;
        StartIndex = startIndex;
        SortingField = sortingField;
        SortingType = sortingType;
    }
}
