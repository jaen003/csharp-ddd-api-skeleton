namespace Src.Core.Shared.Domain.Exceptions;

public abstract class DomainException : CustomException
{
    protected DomainException(int code, string message)
        : base(code, DEBUG, message) { }
}
