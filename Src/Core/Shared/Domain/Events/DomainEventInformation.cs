namespace Src.Core.Shared.Domain.Events;

public class DomainEventInformation
{
    public Type EventClass { get; }
    public List<Type> EventHandlerClasses { get; }
    public string EventName { get; }
    public string ContextName { get; }

    public DomainEventInformation(Type eventClass, List<Type> eventHandlerClasses)
    {
        EventClass = eventClass;
        EventHandlerClasses = eventHandlerClasses;
        EventName = GenerateEventName();
        ContextName = Environment.GetEnvironmentVariable("DOMAIN_EVENT_CONTEXT_NAME")!;
    }

    private string GenerateEventName()
    {
        DomainEvent? _event = (DomainEvent?)Activator.CreateInstance(EventClass);
        return _event!.EventName;
    }

    public bool HasEventHandlers()
    {
        return EventHandlerClasses.Count > 0;
    }
}
