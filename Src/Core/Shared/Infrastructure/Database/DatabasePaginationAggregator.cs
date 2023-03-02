using System.Linq.Dynamic.Core;
using Src.Core.Shared.Domain.Paginations;

namespace Src.Core.Shared.Infrastructure.Database;

public static class DatabasePaginationAggregator
{
    public static IQueryable<T> AddPagination<T>(
        this IQueryable<T> collection,
        Pagination pagination
    )
        where T : class
    {
        if (pagination.HasSorting())
        {
            Sorting sorting = pagination.Sorting!;
            string sortingString = sorting.Field.Value;
            if (sorting.Type.IsDescending())
            {
                sortingString += " DESC";
            }
            if (pagination.HasStartIndex())
            {
                string comparationOperator = ">";
                if (sorting.Type.IsDescending())
                {
                    comparationOperator = "<";
                }
                collection = collection.Where(
                    $"{sorting.Field.Value} {comparationOperator} "
                        + $"\"{pagination.StartIndex!.Value}\""
                );
            }
            collection = collection.OrderBy(sortingString);
        }
        return collection.Take(pagination.Limit.Value);
    }
}
