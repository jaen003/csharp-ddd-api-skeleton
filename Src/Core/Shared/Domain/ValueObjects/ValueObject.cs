namespace Src.Core.Shared.Domain.ValueObjects;

public abstract class ValueObject<T>
{
    public T Value { get; }

    protected ValueObject(T value)
    {
        Value = value;
    }

    public bool Equals(ValueObject<T> other)
    {
        return Value!.Equals(other.Value);
    }

    public bool Equals(T other)
    {
        return Value!.Equals(other);
    }

    public override string ToString()
    {
        return Value!.ToString()!;
    }
}
