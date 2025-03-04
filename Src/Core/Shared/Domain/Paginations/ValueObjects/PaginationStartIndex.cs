using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public record PaginationStartIndex : NonEmptyString
{
    private const string VALUE_OBJECT_NAME = "start index";

    public PaginationStartIndex(string value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PAGINATION) { }
}
