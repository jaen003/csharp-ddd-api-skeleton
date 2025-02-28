namespace Src.Api.V1.InputModels.Paginations;

public class PaginationInputModel
{
    public short Limit { get; set; }

    public string? StartIndex { get; set; }

    public string? SortingField { get; set; }

    public string? SortingType { get; set; }
}
