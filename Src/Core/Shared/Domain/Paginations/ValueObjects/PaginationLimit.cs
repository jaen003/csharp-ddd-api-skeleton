using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public class PaginationLimit : NonNegativeShort
{
    private const short MAXIMUM = 30;
    private const string VALUE_OBJECT_NAME = "limit";

    public PaginationLimit(short value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PAGINATION)
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
