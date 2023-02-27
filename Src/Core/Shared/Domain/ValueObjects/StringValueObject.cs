using System.Text.RegularExpressions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class StringValueObject
{
    public string Value { get; }

    public StringValueObject(string value)
    {
        Value = value;
    }

    public bool IsEmpty()
    {
        return Value.Length == 0;
    }

    public bool Equals(StringValueObject other)
    {
        return Value == other.Value;
    }

    public bool Matches(StringValueObject other)
    {
        return Regex.Match(Value, other.Value).Success;
    }

    public bool IsLongerThan(StringValueObject other)
    {
        return Value.Length > other.Value.Length;
    }

    public bool IsLongerThanOrEqual(StringValueObject other)
    {
        return Value.Length >= other.Value.Length;
    }
}
