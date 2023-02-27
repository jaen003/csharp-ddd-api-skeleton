namespace Src.Core.Shared.Domain.Exceptions;

public class NegativeShortException : ValidationErrorException
{
    private const int CODE = 12;

    public NegativeShortException(int code, string message)
        : base(code, message) { }

    public NegativeShortException() { }

    public NegativeShortException(short nonNegativeShort)
        : base(CODE, $"The short integer '{nonNegativeShort}' must not be negative.") { }

    public NegativeShortException(string? message)
        : base(message) { }

    public NegativeShortException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public NegativeShortException(int code, int type, string message)
        : base(code, type, message) { }
}
