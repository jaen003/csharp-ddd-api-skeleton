namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmailFormatException : CustomDomainException
{
    public InvalidEmailFormatException(string email)
        : base(CustomExceptionCode.InvalidEmailFormat, $"The email '{email}' is invalid.") { }
}
