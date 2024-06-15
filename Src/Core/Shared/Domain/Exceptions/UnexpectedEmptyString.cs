namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedEmptyString : DomainException
{
    public UnexpectedEmptyString()
        : base(CustomExceptionCode.UnexpectedEmptyString, "The string must not be empty.") { }
}
