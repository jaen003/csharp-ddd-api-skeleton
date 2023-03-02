using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class SortingField : NonEmptyStringValueObject
{
    public SortingField(string value)
        : base(value) { }
}
