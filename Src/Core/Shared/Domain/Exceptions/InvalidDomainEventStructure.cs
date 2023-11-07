namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidDomainEventStructure : InternalError
{
    public InvalidDomainEventStructure(string eventName)
        : base($"The structure of the domain event '{eventName}' is invalid.") { }
}
