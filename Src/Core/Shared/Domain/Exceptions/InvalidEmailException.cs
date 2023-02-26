namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmailException : ValidationErrorException
{
    private const int CODE = 6;

    public InvalidEmailException(int code, string message)
        : base(code, message) { }

    public InvalidEmailException()
        : base(CODE, "The string must not be empty.") { }

    public InvalidEmailException(string? message)
        : base(message) { }

    public InvalidEmailException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public InvalidEmailException(int code, int type, string message)
        : base(code, type, message) { }
}
