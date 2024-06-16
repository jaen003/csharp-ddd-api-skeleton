using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class InvalidDomainEventStructure : InfrastructureException
{
    public InvalidDomainEventStructure(string eventName)
        : base(
            CustomExceptionCode.InvalidDomainEventStructure,
            $"The structure of the domain event '{eventName}' is invalid."
        ) { }
}
