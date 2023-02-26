namespace Src.Core.Shared.Domain.ValueObjects;

public class IntValueObject
{
    public int Value { get; }

    public IntValueObject(int value)
    {
        Value = value;
    }

    public bool Equals(IntValueObject other)
    {
        return Value == other.Value;
    }

    public bool IsLessThan(IntValueObject other)
    {
        return Value < other.Value;
    }

    public bool IsGreaterThan(IntValueObject other)
    {
        return Value > other.Value;
    }

    public bool IsLessThanOrEqual(IntValueObject other)
    {
        return Value <= other.Value;
    }

    public bool IsGreaterThanOrEqual(IntValueObject other)
    {
        return Value >= other.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
