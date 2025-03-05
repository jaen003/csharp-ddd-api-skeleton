using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Shared.Domain.Paginations.ValueObjects;

public record PaginationLimit : NonNegativeShort
{
    private const short Maximum = 30;
    private const string ValueObjectName = "limit";

    public PaginationLimit(short value)
        : base(value, ValueObjectName, AggregateName.Pagination)
    {
        if (!IsValid())
        {
            throw new InvalidPaginationLimitException(value);
        }
    }

    private bool IsValid()
    {
        return IsLessThanOrEqual(Maximum);
    }
}
