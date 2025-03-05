using System.Collections.ObjectModel;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Application.Events;

public class DomainEventInformation
{
    public Type EventClass { get; }
    public ReadOnlyCollection<Type> EventHandlerClasses { get; }
    public string EventName { get; }
    public const string ContextName = "backoffice";

    public DomainEventInformation(Type eventClass, ReadOnlyCollection<Type> eventHandlerClasses)
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
