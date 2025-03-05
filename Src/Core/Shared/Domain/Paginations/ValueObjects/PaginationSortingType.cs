using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public record PaginationSortingType : NonEmptyString
{
    private const string Ascending = "asc";
    private const string Descending = "desc";
    private const string ValueObjectName = "type";

    public PaginationSortingType(string? value)
        : base(value ?? Descending, ValueObjectName, AggregateName.PaginationSorting)
    {
        if (!IsValid())
        {
            throw new InvalidPaginationSortingTypeException(Value);
        }
    }

    private bool IsValid()
    {
        return Equals(Ascending) || Equals(Descending);
    }

    public bool IsDescending()
    {
        return Equals(Descending);
    }
}
