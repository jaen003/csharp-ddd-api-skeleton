namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedEmptyStringException : DomainException
{
    public UnexpectedEmptyStringException()
        : base(CustomExceptionCode.UnexpectedEmptyString, "The string must not be empty.") { }
}
