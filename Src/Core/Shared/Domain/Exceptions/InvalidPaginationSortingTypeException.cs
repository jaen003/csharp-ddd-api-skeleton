namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidPaginationSortingTypeException : CustomDomainException
{
    public InvalidPaginationSortingTypeException(string type)
        : base(
            CustomExceptionCode.InvalidPaginationSortingType,
            $"The sorting type '{type}' is invalid."
        ) { }
}
