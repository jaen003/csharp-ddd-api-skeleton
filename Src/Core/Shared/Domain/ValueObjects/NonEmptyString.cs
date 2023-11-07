using Src.Core.Shared.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonEmptyString : ValueObject<string>
{
    public NonEmptyString(string value)
        : base(value)
    {
        if (IsEmpty())
        {
            throw new UnexpectedEmptyString();
        }
    }

    private bool IsEmpty()
    {
        return Value.Length == 0;
    }

    public bool Matches(NonEmptyString other)
    {
        return Regex.Match(Value, other.Value).Success;
    }

    public bool Matches(string other)
    {
        return Regex.Match(Value, other).Success;
    }

    public bool IsLongerThan(NonEmptyString other)
    {
        return Value.Length > other.Value.Length;
    }

    public bool IsLongerThan(string other)
    {
        return Value.Length > other.Length;
    }

    public bool IsLongerThanOrEqual(NonEmptyString other)
    {
        return Value.Length >= other.Value.Length;
    }

    public bool IsLongerThanOrEqual(string other)
    {
        return Value.Length >= other.Length;
    }
}
