namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidSortingTypeException : ValidationErrorException
{
    private const int CODE = 9;

    public InvalidSortingTypeException(int code, string message)
        : base(code, message) { }

    public InvalidSortingTypeException(string type)
        : base(CODE, $"The sorting type '{type}' is invalid.") { }

    public InvalidSortingTypeException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public InvalidSortingTypeException(int code, int type, string message)
        : base(code, type, message) { }

    public InvalidSortingTypeException() { }
}
