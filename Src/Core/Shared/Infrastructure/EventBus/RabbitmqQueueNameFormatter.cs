using Src.Core.Shared.Application.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitmqQueueNameFormatter
{
    private const string DEAD_LETTER_PREFIX = "dead.letter";

    public static string Format(DomainEventInformation eventInformation)
    {
        string contextName = DomainEventInformation.CONTEXT_NAME;
        string eventName = eventInformation.EventName;
        return $"{contextName}.{eventName}";
    }

    public static string FormatToDeadLetter()
    {
        string contextName = DomainEventInformation.CONTEXT_NAME;
        return $"{DEAD_LETTER_PREFIX}.{contextName}";
    }
}
