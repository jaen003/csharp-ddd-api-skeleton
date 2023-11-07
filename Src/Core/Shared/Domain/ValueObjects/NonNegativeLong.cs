using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeLong : ValueObject<long>
{
    private const long MINIMUN = 0;

    public NonNegativeLong(long value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new UnexpectedNegativeLong(value);
        }
    }

    private bool IsNegative()
    {
        return IsLessThan(MINIMUN);
    }

    public bool IsLessThan(NonNegativeLong other)
    {
        return Value < other.Value;
    }

    public bool IsLessThan(long other)
    {
        return Value < other;
    }

    public bool IsGreaterThan(NonNegativeLong other)
    {
        return Value > other.Value;
    }

    public bool IsGreaterThan(long other)
    {
        return Value > other;
    }

    public bool IsLessThanOrEqual(NonNegativeLong other)
    {
        return Value <= other.Value;
    }

    public bool IsLessThanOrEqual(long other)
    {
        return Value <= other;
    }

    public bool IsGreaterThanOrEqual(NonNegativeLong other)
    {
        return Value >= other.Value;
    }

    public bool IsGreaterThanOrEqual(long other)
    {
        return Value >= other;
    }
}
