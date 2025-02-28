using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public class PaginationSortingType : NonEmptyString
{
    private const string ASCENDING = "asc";
    private const string DESCENDING = "desc";
    private const string VALUE_OBJECT_NAME = "type";

    public PaginationSortingType(string? value)
        : base(value ?? DESCENDING, VALUE_OBJECT_NAME, AggregateName.PAGINATION_SORTING)
    {
        if (!IsValid())
        {
            throw new InvalidPaginationSortingTypeException(Value);
        }
    }

    private bool IsValid()
    {
        return Equals(ASCENDING) || Equals(DESCENDING);
    }

    public bool IsDescending()
    {
        return Equals(DESCENDING);
    }
}
