using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations;

public class Pagination
{
    private readonly PaginationLimit limit;
    private readonly PaginationStartIndex? startIndex;
    private readonly Sorting? sorting;

    public string SortingField => sorting!.Field;
    public string StartIndex => startIndex!.Value;
    public int Limit => limit.Value;

    private Pagination(PaginationLimit limit, PaginationStartIndex? startIndex, Sorting? sorting)
    {
        this.limit = limit;
        this.startIndex = startIndex;
        this.sorting = sorting;
    }

    public bool HasSorting()
    {
        return sorting != null;
    }

    public bool HasStartIndex()
    {
        return startIndex != null;
    }

    public bool IsDescendingSortingType()
    {
        return sorting!.IsDescending();
    }

    public static Pagination Create(
        int limit,
        string? startIndex,
        string? sortingField,
        string? sortingType
    )
    {
        PaginationStartIndex? paginationStartIndex = null;
        if (startIndex != null)
        {
            paginationStartIndex = new PaginationStartIndex(startIndex);
        }
        Sorting? sorting = Sorting.Create(sortingField, sortingType);
        if (startIndex != null && sorting == null)
        {
            throw new UnexpectedNullSortingField();
        }
        return new Pagination(new PaginationLimit(limit), paginationStartIndex, sorting);
    }
}
