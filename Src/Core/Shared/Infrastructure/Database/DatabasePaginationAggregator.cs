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
            string sortingString = pagination.SortingField;
            if (pagination.IsDescendingSortingType())
            {
                sortingString += " DESC";
            }
            if (pagination.HasStartIndex())
            {
                string comparationOperator = ">";
                if (pagination.IsDescendingSortingType())
                {
                    comparationOperator = "<";
                }
                collection = collection.Where(
                    $"{pagination.SortingField} {comparationOperator} "
                        + $"\"{pagination.StartIndex}\""
                );
            }
            collection = collection.OrderBy(sortingString);
        }
        return collection.Take(pagination.Limit);
    }
}
