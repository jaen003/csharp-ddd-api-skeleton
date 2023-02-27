namespace Src.Core.Shared.Domain.Exceptions;

public class InternalErrorException : DomainException
{
    private const int CODE = 2;

    public InternalErrorException(string message)
        : base(CODE, CRITICAL, message) { }

    public InternalErrorException() { }

    public InternalErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public InternalErrorException(int code, int type, string message)
        : base(code, type, message) { }
}
