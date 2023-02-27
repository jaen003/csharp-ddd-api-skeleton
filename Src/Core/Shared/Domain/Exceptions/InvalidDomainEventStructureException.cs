namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidDomainEventStructureException : InternalErrorException
{
    public InvalidDomainEventStructureException() { }

    public InvalidDomainEventStructureException(string eventName)
        : base($"the structure of the domain event '{eventName}' is invalid.") { }

    public InvalidDomainEventStructureException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public InvalidDomainEventStructureException(int code, int type, string message)
        : base(code, type, message) { }
}
