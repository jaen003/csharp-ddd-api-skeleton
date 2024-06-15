namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidSortingType : DomainException
{
    public InvalidSortingType(string type)
        : base(CustomExceptionCode.InvalidSortingType, $"The sorting type '{type}' is invalid.") { }
}
