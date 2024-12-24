using Src.Core.Shared.Application.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitmqExchangeNameFormatter
{
    private const string DEAD_LETTER_PREFIX = "dead.letter";
    private const string RETRY_PREFIX = "retry";

    public static string Format(DomainEventInformation eventInformation)
    {
        return eventInformation.EventName;
    }

    public static string FormatToDeadLetter()
    {
        string contextName = DomainEventInformation.CONTEXT_NAME;
        return $"{DEAD_LETTER_PREFIX}.{contextName}";
    }

    public static string FormatToRetry(DomainEventInformation eventInformation)
    {
        string contextName = DomainEventInformation.CONTEXT_NAME;
        string eventName = eventInformation.EventName;
        return $"{RETRY_PREFIX}.{contextName}.{eventName}";
    }
}
