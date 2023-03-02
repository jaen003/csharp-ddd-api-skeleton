using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class PaginationLimit : NonNegativeIntValueObject
{
    private const int MAXIMUM = 30;

    public PaginationLimit(int value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidPaginationLimitException(value);
        }
    }

    private bool IsValid()
    {
        return IsLessThanOrEqual(new IntValueObject(MAXIMUM));
    }
}
