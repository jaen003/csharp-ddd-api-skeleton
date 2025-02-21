namespace Src.Core.Shared.Domain.Exceptions;

public abstract class CustomDomainException : CustomException
{
    protected CustomDomainException(CustomExceptionCode code, string message)
        : base(code, CustomExceptionSeverityLevel.Error, message) { }
}
