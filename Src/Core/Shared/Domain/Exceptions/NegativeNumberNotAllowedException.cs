namespace Src.Core.Shared.Domain.Exceptions;

public class NegativeNumberNotAllowedException<T> : CustomDomainException
{
    public NegativeNumberNotAllowedException(T number)
        : base(
            CustomExceptionCode.NegativeNumberNotAllowed,
            $"The number '{number}' must not be negative."
        ) { }
}
