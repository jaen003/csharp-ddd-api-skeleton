namespace Src.Core.Shared.Domain.Exceptions;

public class DatabaseErrorException : InternalErrorException
{
    public DatabaseErrorException(string message)
        : base($"An internal database error occurred during the request: {message}.") { }

    public DatabaseErrorException() { }

    public DatabaseErrorException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public DatabaseErrorException(int code, int type, string message)
        : base(code, type, message) { }
}
