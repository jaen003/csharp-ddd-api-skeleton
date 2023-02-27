using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitmqQueueNameFormatter
{
    private const string DEAD_LETTER_PREFIX = "dead.letter";

    public static string Format(DomainEventInformation eventInformation)
    {
        string contextName = eventInformation.ContextName;
        string eventName = eventInformation.EventName;
        return $"{contextName}.{eventName}";
    }

    public static string FormatToDeadLetter(DomainEventInformation eventInformation)
    {
        string contextName = eventInformation.ContextName;
        return $"{DEAD_LETTER_PREFIX}.{contextName}";
    }
}
