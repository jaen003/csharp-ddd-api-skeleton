namespace Src.Core.Shared.Domain.Exceptions;

public abstract class ValidationError : ApplicationException
{
    protected ValidationError(int code, string message)
        : base(code, DEBUG, message) { }
}
