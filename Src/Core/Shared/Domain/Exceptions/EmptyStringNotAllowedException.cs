namespace Src.Core.Shared.Domain.Exceptions;

public class EmptyStringNotAllowedException : CustomDomainException
{
    public EmptyStringNotAllowedException(string valueObjectName, string aggregateName)
        : base(
            CustomExceptionCode.EmptyStringNotAllowed,
            $"The {aggregateName} {valueObjectName} must not be empty."
        ) { }
}
