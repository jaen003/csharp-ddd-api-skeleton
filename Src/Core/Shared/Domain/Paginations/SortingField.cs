using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class SortingField : NonEmptyString
{
    public SortingField(string value)
        : base(value) { }
}
