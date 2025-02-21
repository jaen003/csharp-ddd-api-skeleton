using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public class PaginationSortingField : NonEmptyString
{
    public PaginationSortingField(string value)
        : base(value) { }
}
