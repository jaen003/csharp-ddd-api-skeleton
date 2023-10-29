namespace Src.Core.Shared.Domain.Exceptions;

public class MultipleApplicationException : Exception
{
    public List<ApplicationException> Exceptions { get; }

    public MultipleApplicationException(List<ApplicationException> exceptions)
    {
        Exceptions = exceptions;
    }
}
