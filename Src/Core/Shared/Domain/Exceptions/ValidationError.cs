namespace Src.Core.Shared.Domain.Exceptions;

public abstract class ValidationError : CustomException
{
    protected ValidationError(int code, string message)
        : base(code, DEBUG, message) { }
}
