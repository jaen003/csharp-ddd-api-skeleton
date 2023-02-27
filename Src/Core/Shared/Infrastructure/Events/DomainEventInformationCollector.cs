using Microsoft.Extensions.DependencyInjection;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.Events;

public static class DomainEventInformationCollector
{
    public static IServiceCollection CollectDomainEventInformation(this IServiceCollection services)
    {
        DomainEventInformationCollection eventInformationCollection = new();
        foreach (Type eventClass in DomainEventFinder.Find())
        {
            List<Type> eventHandlerClasses = DomainEventHandlerFinder.FindByEventClass(eventClass);
            foreach (Type eventHandlerClass in eventHandlerClasses)
            {
                services.AddTransient(eventHandlerClass, eventHandlerClass);
            }
            eventInformationCollection.Add(
                new DomainEventInformation(eventClass, eventHandlerClasses)
            );
        }
        services.AddSingleton(eventInformationCollection);
        return services;
    }
}
