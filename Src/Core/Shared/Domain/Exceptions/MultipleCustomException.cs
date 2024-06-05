namespace Src.Core.Shared.Domain.Exceptions;

public class MultipleCustomException : Exception
{
    public List<CustomException> Exceptions { get; }

    public MultipleCustomException(List<CustomException> exceptions)
    {
        Exceptions = exceptions;
    }
}
