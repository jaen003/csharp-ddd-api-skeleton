using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class EventBusNotConfiguredException : InfrastructureException
{
    public EventBusNotConfiguredException(string messageDetails)
        : base(
            CustomExceptionCode.EventBusNotConfigured,
            $"The event bus could not be configured: {messageDetails}."
        ) { }
}
