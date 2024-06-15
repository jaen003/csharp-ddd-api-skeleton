namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmail : DomainException
{
    public InvalidEmail(string email)
        : base(CustomExceptionCode.InvalidEmail, $"The email '{email}' is invalid.") { }
}
