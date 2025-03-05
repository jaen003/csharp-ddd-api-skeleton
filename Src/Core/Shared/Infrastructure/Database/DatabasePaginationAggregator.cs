using System.Linq.Dynamic.Core;
using Src.Core.Shared.Domain.Paginations.Aggregates;

namespace Src.Core.Shared.Infrastructure.Database;

public static class DatabasePaginationAggregator
{
    private const string DescendingSortType = "DESC";
    private const string GreaterThanComparasionOperator = ">";
    private const string LessThanComparasionOperator = "<";

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
                sortingString += $" {DescendingSortType}";
            }
            if (pagination.HasStartIndex())
            {
                string comparasionOperator = GreaterThanComparasionOperator;
                if (pagination.IsDescendingSortingType())
                {
                    comparasionOperator = LessThanComparasionOperator;
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
