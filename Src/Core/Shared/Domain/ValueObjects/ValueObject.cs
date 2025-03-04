namespace Src.Core.Shared.Domain.ValueObjects;

public abstract record ValueObject<T>
    where T : notnull
{
    public T Value { get; }

    protected ValueObject(T value)
    {
        Value = value;
    }

    public bool Equals(T other)
    {
        return Value.Equals(other);
    }
}
