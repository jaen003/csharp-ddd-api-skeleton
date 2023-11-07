using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class PaginationStartIndex : NonEmptyString
{
    public PaginationStartIndex(string value)
        : base(value) { }
}
