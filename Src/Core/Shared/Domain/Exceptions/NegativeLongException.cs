namespace Src.Core.Shared.Domain.Exceptions;

public class NegativeLongException : ValidationErrorException
{
    private const int CODE = 11;

    public NegativeLongException(int code, string message)
        : base(code, message) { }

    public NegativeLongException() { }

    public NegativeLongException(long nonNegativelong)
        : base(CODE, $"The long integer '{nonNegativelong}' must not be negative.") { }

    public NegativeLongException(string? message)
        : base(message) { }

    public NegativeLongException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public NegativeLongException(int code, int type, string message)
        : base(code, type, message) { }
}
