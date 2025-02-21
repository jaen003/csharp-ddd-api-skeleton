using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class DomainEventConsumptionFailedException : InfrastructureException
{
    public DomainEventConsumptionFailedException(string messageDetails)
        : base(
            CustomExceptionCode.DomainEventConsumptionFailed,
            $"The domain event could not be consumed: {messageDetails}."
        ) { }
}
