using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public class PaginationLimit : NonNegativeInt
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
        return IsLessThanOrEqual(MAXIMUM);
    }
}
