namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeInt : ValidationError
{
    private const int CODE = 4;

    public UnexpectedNegativeInt(int nonNegativeInt)
        : base(CODE, $"The integer '{nonNegativeInt}' must not be negative.") { }
}
