namespace Src.Core.Shared.Domain.Exceptions;

public class EmptyStringNotAllowedException : CustomDomainException
{
    public EmptyStringNotAllowedException()
        : base(CustomExceptionCode.EmptyStringNotAllowed, "The string must not be empty.") { }
}
