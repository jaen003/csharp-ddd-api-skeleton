namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidUuid : DomainException
{
    private const int CODE = 13;

    public InvalidUuid(string uuid)
        : base(CODE, $"The uuid '{uuid}' is invalid.") { }
}
