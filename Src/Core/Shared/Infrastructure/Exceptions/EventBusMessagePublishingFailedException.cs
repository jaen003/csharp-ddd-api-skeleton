using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Infrastructure.Exceptions;

public class EventBusMessagePublishingFailedException : InfrastructureException
{
    public EventBusMessagePublishingFailedException(string messageDetails)
        : base(
            CustomExceptionCode.EventBusMessagePublishingFailed,
            $"The message could not be published in the event bus: {messageDetails}."
        ) { }
}
