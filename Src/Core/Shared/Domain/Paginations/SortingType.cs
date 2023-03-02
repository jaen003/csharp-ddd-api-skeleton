using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class SortingType : NonEmptyStringValueObject
{
    private const string ASCENDING = "asc";
    private const string DESCENDING = "desc";

    public SortingType(string? value)
        : base(value ?? DESCENDING)
    {
        if (!IsValid())
        {
            throw new InvalidSortingTypeException(Value);
        }
    }

    private bool IsValid()
    {
        return Equals(new StringValueObject(ASCENDING))
            || Equals(new StringValueObject(DESCENDING));
    }

    public bool IsDescending()
    {
        return Equals(new StringValueObject(DESCENDING));
    }
}
