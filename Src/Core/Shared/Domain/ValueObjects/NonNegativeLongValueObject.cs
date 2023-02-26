using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeLongValueObject : LongValueObject
{
    private const long MINIMUN = 0;

    public NonNegativeLongValueObject(long value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new NegativeLongException(value);
        }
    }

    public bool IsNegative()
    {
        return IsLessThan(new LongValueObject(MINIMUN));
    }
}
