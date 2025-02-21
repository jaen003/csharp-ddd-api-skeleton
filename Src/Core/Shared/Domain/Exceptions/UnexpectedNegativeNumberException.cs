namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeNumberException<T> : DomainException
{
    public UnexpectedNegativeNumberException(T number)
        : base(
            CustomExceptionCode.UnexpectedNegativeNumber,
            $"The number '{number}' must not be negative."
        ) { }
}
