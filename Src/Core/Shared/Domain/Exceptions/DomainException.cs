namespace Src.Core.Shared.Domain.Exceptions;

public abstract class DomainException : CustomException
{
    protected DomainException(CustomExceptionCode code, string message)
        : base(code, SeverityLevel.Debug, message) { }
}
