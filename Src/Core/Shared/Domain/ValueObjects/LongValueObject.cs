namespace Src.Core.Shared.Domain.ValueObjects;

public class LongValueObject
{
    public long Value { get; }

    public LongValueObject(long value)
    {
        Value = value;
    }

    public bool Equals(LongValueObject other)
    {
        return Value == other.Value;
    }

    public bool IsLessThan(LongValueObject other)
    {
        return Value < other.Value;
    }

    public bool IsGreaterThan(LongValueObject other)
    {
        return Value > other.Value;
    }

    public bool IsLessThanOrEqual(LongValueObject other)
    {
        return Value <= other.Value;
    }

    public bool IsGreaterThanOrEqual(LongValueObject other)
    {
        return Value >= other.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
