namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidSortingTypeException : DomainException
{
    public InvalidSortingTypeException(string type)
        : base(CustomExceptionCode.InvalidSortingType, $"The sorting type '{type}' is invalid.") { }
}
