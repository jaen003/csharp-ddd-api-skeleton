namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidEmailFormatException : CustomDomainException
{
    public InvalidEmailFormatException(string email, string aggregateName)
        : base(
            CustomExceptionCode.InvalidEmailFormat,
            $"The {aggregateName} email '{email}' has an invalid format."
        ) { }
}
