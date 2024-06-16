using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class DomainEventConsumptionFailed : InfrastructureException
{
    public DomainEventConsumptionFailed(string messageDetails)
        : base(
            CustomExceptionCode.DomainEventConsumptionFailed,
            $"The domain event could not be consumed: {messageDetails}."
        ) { }
}
