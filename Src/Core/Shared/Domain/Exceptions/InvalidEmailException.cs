namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmailException : DomainException
{
    public InvalidEmailException(string email)
        : base(CustomExceptionCode.InvalidEmail, $"The email '{email}' is invalid.") { }
}
