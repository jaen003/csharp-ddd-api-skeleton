using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations;

public class PaginationLimit : NonNegativeInt
{
    private const int MAXIMUM = 30;

    public PaginationLimit(int value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidPaginationLimit(value);
        }
    }

    private bool IsValid()
    {
        return IsLessThanOrEqual(MAXIMUM);
    }
}
