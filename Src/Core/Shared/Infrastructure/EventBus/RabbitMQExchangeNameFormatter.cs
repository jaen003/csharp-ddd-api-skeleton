using Src.Core.Shared.Application.Events;

namespace Src.Core.Shared.Infrastructure.EventBus;

public static class RabbitMQExchangeNameFormatter
{
    private const string DeadLetterPrefix = "dead.letter";
    private const string RetryPrefix = "retry";

    public static string Format(DomainEventInformation eventInformation)
    {
        return eventInformation.EventName;
    }

    public static string FormatToDeadLetter()
    {
        string contextName = DomainEventInformation.ContextName;
        return $"{DeadLetterPrefix}.{contextName}";
    }

    public static string FormatToRetry(DomainEventInformation eventInformation)
    {
        string contextName = DomainEventInformation.ContextName;
        string eventName = eventInformation.EventName;
        return $"{RetryPrefix}.{contextName}.{eventName}";
    }
}
