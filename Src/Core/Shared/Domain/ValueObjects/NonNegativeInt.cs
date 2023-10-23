using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeInt : ValueObject<int>
{
    private const int MINIMUN = 0;

    public NonNegativeInt(int value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new NegativeIntException(value);
        }
    }

    private bool IsNegative()
    {
        return IsLessThan(MINIMUN);
    }

    public bool IsLessThan(NonNegativeInt other)
    {
        return Value < other.Value;
    }

    public bool IsLessThan(int other)
    {
        return Value < other;
    }

    public bool IsGreaterThan(NonNegativeInt other)
    {
        return Value > other.Value;
    }

    public bool IsGreaterThan(int other)
    {
        return Value > other;
    }

    public bool IsLessThanOrEqual(NonNegativeInt other)
    {
        return Value <= other.Value;
    }

    public bool IsLessThanOrEqual(int other)
    {
        return Value <= other;
    }

    public bool IsGreaterThanOrEqual(NonNegativeInt other)
    {
        return Value >= other.Value;
    }

    public bool IsGreaterThanOrEqual(int other)
    {
        return Value >= other;
    }
}
