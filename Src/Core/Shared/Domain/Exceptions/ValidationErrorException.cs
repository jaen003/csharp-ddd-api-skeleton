namespace Src.Core.Shared.Domain.Exceptions;

public class ValidationErrorException : DomainException
{
    public ValidationErrorException(int code, string message)
        : base(code, DEBUG, message) { }

    public ValidationErrorException() { }

    public ValidationErrorException(string? message)
        : base(message) { }

    public ValidationErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public ValidationErrorException(int code, int type, string message)
        : base(code, type, message) { }
}
