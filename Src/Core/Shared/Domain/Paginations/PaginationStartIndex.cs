using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class PaginationStartIndex : NonEmptyStringValueObject
{
    public PaginationStartIndex(string value)
        : base(value) { }
}
