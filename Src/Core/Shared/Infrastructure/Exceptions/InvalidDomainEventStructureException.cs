using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class InvalidDomainEventStructureException : InfrastructureException
{
    public InvalidDomainEventStructureException(string eventName)
        : base(
            CustomExceptionCode.InvalidDomainEventStructure,
            $"The structure of the domain event '{eventName}' is invalid."
        ) { }
}
