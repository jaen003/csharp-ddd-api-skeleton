using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public record PaginationStartIndex : NonEmptyString
{
    private const string ValueObjectName = "start index";

    public PaginationStartIndex(string value)
        : base(value, ValueObjectName, AggregateName.Pagination) { }
}
