namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeShort : ValidationError
{
    private const int CODE = 12;

    public UnexpectedNegativeShort(short nonNegativeShort)
        : base(CODE, $"The short integer '{nonNegativeShort}' must not be negative.") { }
}
