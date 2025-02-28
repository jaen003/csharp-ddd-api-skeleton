namespace Src.Core.Shared.Domain.Exceptions;

public class NegativeNumberNotAllowedException<T> : CustomDomainException
{
    public NegativeNumberNotAllowedException(T number, string valueObjectName, string aggregateName)
        : base(
            CustomExceptionCode.NegativeNumberNotAllowed,
            $"The {aggregateName} {valueObjectName} '{number}' must not be negative."
        ) { }
}
