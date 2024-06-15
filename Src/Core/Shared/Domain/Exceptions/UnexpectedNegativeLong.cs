namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNegativeLong : DomainException
{
    public UnexpectedNegativeLong(long nonNegativelong)
        : base(
            CustomExceptionCode.UnexpectedNegativeLong,
            $"The long integer '{nonNegativelong}' must not be negative."
        ) { }
}
