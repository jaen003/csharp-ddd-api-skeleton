using System.Collections.ObjectModel;

namespace Src.Core.Shared.Domain.Exceptions;

public class MultipleCustomException : Exception
{
    public ReadOnlyCollection<CustomException> Exceptions { get; }

    public MultipleCustomException(ReadOnlyCollection<CustomException> exceptions)
    {
        Exceptions = exceptions;
    }
}
