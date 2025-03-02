using System.Collections.ObjectModel;
using System.Reflection;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Shared.Infrastructure.Events;

public static class DomainEventFinder
{
    public static ReadOnlyCollection<Type> Find()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        IEnumerable<Type> satisfiedClasses = assembly.ExportedTypes.Where(i =>
            i.IsClass && !i.IsAbstract && i.IsSubclassOf(typeof(DomainEvent))
        );
        return satisfiedClasses.ToList().AsReadOnly();
    }
}
