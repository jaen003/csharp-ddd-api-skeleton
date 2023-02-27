namespace Src.Core.Shared.Domain.ValueObjects;

public class ShortValueObject
{
    public short Value { get; }

    public ShortValueObject(short value)
    {
        Value = value;
    }

    public bool Equals(ShortValueObject other)
    {
        return Value == other.Value;
    }

    public bool IsLessThan(ShortValueObject other)
    {
        return Value < other.Value;
    }

    public bool IsGreaterThan(ShortValueObject other)
    {
        return Value > other.Value;
    }

    public bool IsLessThanOrEqual(ShortValueObject other)
    {
        return Value <= other.Value;
    }

    public bool IsGreaterThanOrEqual(ShortValueObject other)
    {
        return Value >= other.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
