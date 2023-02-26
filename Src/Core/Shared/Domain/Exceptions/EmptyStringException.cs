namespace Src.Core.Shared.Domain.Exceptions;

public class EmptyStringException : ValidationErrorException
{
    private const int CODE = 5;

    public EmptyStringException(int code, string message)
        : base(code, message) { }

    public EmptyStringException()
        : base(CODE, "The string must not be empty.") { }

    public EmptyStringException(string? message)
        : base(message) { }

    public EmptyStringException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public EmptyStringException(int code, int type, string message)
        : base(code, type, message) { }
}
