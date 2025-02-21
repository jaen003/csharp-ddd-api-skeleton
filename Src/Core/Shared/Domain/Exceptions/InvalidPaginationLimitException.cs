namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidPaginationLimitException : CustomDomainException
{
    public InvalidPaginationLimitException(int limit)
        : base(
            CustomExceptionCode.InvalidPaginationLimit,
            $"The pagination limit '{limit}' is invalid."
        ) { }
}
