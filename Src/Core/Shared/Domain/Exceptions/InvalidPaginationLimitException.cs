namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidPaginationLimitException : ValidationErrorException
{
    private const int CODE = 7;

    public InvalidPaginationLimitException(int code, string message)
        : base(code, message) { }

    public InvalidPaginationLimitException(int limit)
        : base(CODE, $"The pagination limit '{limit}' is invalid.") { }

    public InvalidPaginationLimitException(string? message)
        : base(message) { }

    public InvalidPaginationLimitException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public InvalidPaginationLimitException(int code, int type, string message)
        : base(code, type, message) { }

    public InvalidPaginationLimitException() { }
}
