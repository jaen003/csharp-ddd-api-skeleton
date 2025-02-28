using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public abstract class NonNegativeNumber<T> : ValueObject<T>
    where T : struct, IComparable<T>
{
    protected NonNegativeNumber(T value, string valueObjectName, string aggregateName)
        : base(value)
    {
        if (IsNegative())
        {
            throw new NegativeNumberNotAllowedException<T>(value, valueObjectName, aggregateName);
        }
    }

    protected abstract bool IsNegative();

    public bool IsLessThan(NonNegativeNumber<T> other)
    {
        return Value.CompareTo(other.Value) < 0;
    }

    public bool IsLessThan(T other)
    {
        return Value.CompareTo(other) < 0;
    }

    public bool IsGreaterThan(NonNegativeNumber<T> other)
    {
        return Value.CompareTo(other.Value) > 0;
    }

    public bool IsGreaterThan(T other)
    {
        return Value.CompareTo(other) > 0;
    }

    public bool IsLessThanOrEqual(NonNegativeNumber<T> other)
    {
        return Value.CompareTo(other.Value) <= 0;
    }

    public bool IsLessThanOrEqual(T other)
    {
        return Value.CompareTo(other) <= 0;
    }

    public bool IsGreaterThanOrEqual(NonNegativeNumber<T> other)
    {
        return Value.CompareTo(other.Value) >= 0;
    }

    public bool IsGreaterThanOrEqual(T other)
    {
        return Value.CompareTo(other) >= 0;
    }
}
