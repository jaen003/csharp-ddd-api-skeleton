namespace Src.Core.Shared.Domain.Exceptions;

public class NullSortingFieldException : ValidationErrorException
{
    private const int CODE = 10;

    public NullSortingFieldException(int code, string message)
        : base(code, message) { }

    public NullSortingFieldException()
        : base(CODE, "The sorting field must not be null.") { }

    public NullSortingFieldException(string? message)
        : base(message) { }

    public NullSortingFieldException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public NullSortingFieldException(int code, int type, string message)
        : base(code, type, message) { }
}
