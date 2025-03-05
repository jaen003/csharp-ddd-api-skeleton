using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public record PaginationSortingField : NonEmptyString
{
    private const string ValueObjectName = "field";

    public PaginationSortingField(string value)
        : base(value, ValueObjectName, AggregateName.PaginationSorting) { }
}
