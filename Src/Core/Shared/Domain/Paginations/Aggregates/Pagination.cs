using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.Paginations.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.Aggregates;

public class Pagination
{
    private readonly PaginationLimit limit;
    private readonly PaginationStartIndex? startIndex;
    private readonly PaginationSorting? sorting;

    public string SortingField => sorting!.Field;
    public string StartIndex => startIndex!.Value;
    public int Limit => limit.Value;

    private Pagination(
        PaginationLimit limit,
        PaginationStartIndex? startIndex,
        PaginationSorting? sorting
    )
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
        PaginationSorting? paginationSorting = PaginationSorting.Create(sortingField, sortingType);
        if (startIndex != null && paginationSorting == null)
        {
            throw new NullPaginationSortingFieldNotAllowedException();
        }
        return new Pagination(new PaginationLimit(limit), paginationStartIndex, paginationSorting);
    }
}
