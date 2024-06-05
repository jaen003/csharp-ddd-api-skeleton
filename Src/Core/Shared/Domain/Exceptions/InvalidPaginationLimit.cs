namespace Src.Core.Shared.Domain.Exceptions;

public class InvalidPaginationLimit : DomainException
{
    private const int CODE = 7;

    public InvalidPaginationLimit(int limit)
        : base(CODE, $"The pagination limit '{limit}' is invalid.") { }
}
