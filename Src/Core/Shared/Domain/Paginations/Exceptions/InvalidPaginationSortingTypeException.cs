using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations.Exceptions;

public class InvalidPaginationSortingTypeException : CustomDomainException
{
    public InvalidPaginationSortingTypeException(string type)
        : base(
            CustomExceptionCode.InvalidPaginationSortingType,
            $"The pagination sorting type '{type}' is invalid."
        ) { }
}
