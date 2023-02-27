using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitmqExchangeNameFormatter
{
    private const string DEAD_LETTER_PREFIX = "dead.letter";
    private const string RETRY_PREFIX = "retry";

    public static string Format(DomainEventInformation eventInformation)
    {
        return eventInformation.EventName;
    }

    public static string FormatToDeadLetter(DomainEventInformation eventInformation)
    {
        string contextName = eventInformation.ContextName;
        return $"{DEAD_LETTER_PREFIX}.{contextName}";
    }

    public static string FormatToRetry(DomainEventInformation eventInformation)
    {
        string contextName = eventInformation.ContextName;
        string eventName = eventInformation.EventName;
        return $"{RETRY_PREFIX}.{contextName}.{eventName}";
    }
}
