using System.Reflection;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.Events;

public static class DomainEventHandlerFinder
{
    public static List<Type> FindByEventClass(Type eventClass)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> satisfiedClasses = assembly.ExportedTypes.Where(
            i =>
                i.IsClass
                && !i.IsAbstract
                && typeof(IDomainEventHandlerBase).IsAssignableFrom(i)
                && i.GetInterfaces().Any(j => j.GenericTypeArguments.FirstOrDefault() == eventClass)
        );
        return satisfiedClasses.ToList();
    }
}
