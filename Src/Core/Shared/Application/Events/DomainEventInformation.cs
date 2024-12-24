using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.Events;

public class DomainEventInformation
{
    public Type EventClass { get; }
    public List<Type> EventHandlerClasses { get; }
    public string EventName { get; }
    public const string CONTEXT_NAME = "backoffice";

    public DomainEventInformation(Type eventClass, List<Type> eventHandlerClasses)
    {
        EventClass = eventClass;
        EventHandlerClasses = eventHandlerClasses;
        EventName = GenerateEventName();
    }

    private string GenerateEventName()
    {
        DomainEvent? domainEvent = (DomainEvent?)Activator.CreateInstance(EventClass);
        return domainEvent!.EventName;
    }

    public bool HasEventHandlers()
    {
        return EventHandlerClasses.Count > 0;
    }
}
