namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidUuid : DomainException
{
    public InvalidUuid(string uuid)
        : base(CustomExceptionCode.InvalidUuid, $"The uuid '{uuid}' is invalid.") { }
}
