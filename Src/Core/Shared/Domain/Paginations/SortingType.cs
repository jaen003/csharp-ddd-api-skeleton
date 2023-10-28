using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class SortingType : NonEmptyString
{
    private const string ASCENDING = "asc";
    private const string DESCENDING = "desc";

    public SortingType(string? value)
        : base(value ?? DESCENDING)
    {
        if (!IsValid())
        {
            throw new InvalidSortingType(Value);
        }
    }

    private bool IsValid()
    {
        return Equals(ASCENDING) || Equals(DESCENDING);
    }

    public bool IsDescending()
    {
        return Equals(DESCENDING);
    }
}
