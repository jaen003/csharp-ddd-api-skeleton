using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeShort : ValueObject<short>
{
    private const short MINIMUN = 0;

    public NonNegativeShort(short value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new UnexpectedNegativeShort(value);
        }
    }

    private bool IsNegative()
    {
        return IsLessThan(MINIMUN);
    }

    public bool IsLessThan(NonNegativeShort other)
    {
        return Value < other.Value;
    }

    public bool IsLessThan(short other)
    {
        return Value < other;
    }

    public bool IsGreaterThan(NonNegativeShort other)
    {
        return Value > other.Value;
    }

    public bool IsGreaterThan(short other)
    {
        return Value > other;
    }

    public bool IsLessThanOrEqual(NonNegativeShort other)
    {
        return Value <= other.Value;
    }

    public bool IsLessThanOrEqual(short other)
    {
        return Value <= other;
    }

    public bool IsGreaterThanOrEqual(NonNegativeShort other)
    {
        return Value >= other.Value;
    }

    public bool IsGreaterThanOrEqual(short other)
    {
        return Value >= other;
    }
}
