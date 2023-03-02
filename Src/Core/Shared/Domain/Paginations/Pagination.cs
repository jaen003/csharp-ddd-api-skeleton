using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations;

public class Pagination
{
    public PaginationLimit Limit { get; }
    public PaginationStartIndex? StartIndex { get; }
    public Sorting? Sorting { get; }

    public Pagination(PaginationLimit limit, PaginationStartIndex? startIndex, Sorting? sorting)
    {
        Limit = limit;
        StartIndex = startIndex;
        Sorting = sorting;
    }

    public bool HasSorting()
    {
        return Sorting != null;
    }

    public bool HasStartIndex()
    {
        return StartIndex != null;
    }

    public static Pagination FromPrimitives(
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
            throw new NullSortingFieldException();
        }
        return new Pagination(new PaginationLimit(limit), paginationStartIndex, sorting);
    }
}
