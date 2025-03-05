using Src.Core.Shared.Application.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitMQQueueNameFormatter
{
    private const string DeadLetterPrefix = "dead.letter";

    public static string Format(DomainEventInformation eventInformation)
    {
        string contextName = DomainEventInformation.ContextName;
        string eventName = eventInformation.EventName;
        return $"{contextName}.{eventName}";
    }

    public static string FormatToDeadLetter()
    {
        string contextName = DomainEventInformation.ContextName;
        return $"{DeadLetterPrefix}.{contextName}";
    }
}
