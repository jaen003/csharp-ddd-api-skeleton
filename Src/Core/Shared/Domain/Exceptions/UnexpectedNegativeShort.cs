namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeShort : DomainException
{
    public UnexpectedNegativeShort(short nonNegativeShort)
        : base(
            CustomExceptionCode.UnexpectedNegativeShort,
            $"The short integer '{nonNegativeShort}' must not be negative."
        ) { }
}
