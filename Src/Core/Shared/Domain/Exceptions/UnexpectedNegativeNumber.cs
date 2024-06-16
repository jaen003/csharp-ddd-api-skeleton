namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeNumber<T> : DomainException
{
    public UnexpectedNegativeNumber(T number)
        : base(
            CustomExceptionCode.UnexpectedNegativeNumber,
            $"The number '{number}' must not be negative."
        ) { }
}
