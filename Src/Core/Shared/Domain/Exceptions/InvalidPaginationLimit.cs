namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidPaginationLimit : DomainException
{
    public InvalidPaginationLimit(int limit)
        : base(
            CustomExceptionCode.InvalidPaginationLimit,
            $"The pagination limit '{limit}' is invalid."
        ) { }
}
