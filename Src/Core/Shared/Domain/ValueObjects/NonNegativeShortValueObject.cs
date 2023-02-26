using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeShortValueObject : ShortValueObject
{
    private const short MINIMUN = 0;

    public NonNegativeShortValueObject(short value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new NegativeShortException(value);
        }
    }

    public bool IsNegative()
    {
        return IsLessThan(new ShortValueObject(MINIMUN));
    }
}
