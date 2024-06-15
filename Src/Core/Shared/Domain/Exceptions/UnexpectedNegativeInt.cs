namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeInt : DomainException
{
    public UnexpectedNegativeInt(int nonNegativeInt)
        : base(
            CustomExceptionCode.UnexpectedNegativeInt,
            $"The integer '{nonNegativeInt}' must not be negative."
        ) { }
}
