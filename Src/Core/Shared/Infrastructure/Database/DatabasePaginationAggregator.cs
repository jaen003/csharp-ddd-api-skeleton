using System.Linq.Dynamic.Core;
using Src.Core.Shared.Domain.Paginations.Aggregates;

namespace Src.Core.Shared.Infrastructure.Database;

public static class DatabasePaginationAggregator
{
    private const string DESCENDING_SORT_TYPE = "DESC";
    private const string GREATER_THAN_COMPARASION_OPERATOR = ">";
    private const string LESS_THAN_COMPARASION_OPERATOR = "<";

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
                sortingString += $" {DESCENDING_SORT_TYPE}";
            }
            if (pagination.HasStartIndex())
            {
                string comparasionOperator = GREATER_THAN_COMPARASION_OPERATOR;
                if (pagination.IsDescendingSortingType())
                {
                    comparasionOperator = LESS_THAN_COMPARASION_OPERATOR;
                }
                collection = collection.Where(
                    $"{pagination.SortingField} {comparasionOperator} "
                        + $"\"{pagination.StartIndex}\""
                );
            }
            collection = collection.OrderBy(sortingString);
        }
        return collection.Take(pagination.Limit);
    }
}
