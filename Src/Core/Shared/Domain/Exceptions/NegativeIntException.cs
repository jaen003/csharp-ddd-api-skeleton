namespace Src.Core.Shared.Domain.Exceptions;

public class NegativeIntException : ValidationErrorException
{
    private const int CODE = 4;

    public NegativeIntException(int code, string message)
        : base(code, message) { }

    public NegativeIntException(int nonNegativeInt)
        : base(CODE, $"The integer '{nonNegativeInt}' must not be negative.") { }

    public NegativeIntException() { }

    public NegativeIntException(string? message)
        : base(message) { }

    public NegativeIntException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public NegativeIntException(int code, int type, string message)
        : base(code, type, message) { }
}
