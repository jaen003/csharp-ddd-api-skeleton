namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidUuidException : CustomDomainException
{
    public InvalidUuidException(string uuid)
        : base(CustomExceptionCode.InvalidUuid, $"The uuid '{uuid}' is invalid.") { }
}
