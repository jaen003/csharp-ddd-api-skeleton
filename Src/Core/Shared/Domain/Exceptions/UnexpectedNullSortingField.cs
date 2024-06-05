namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNullSortingField : DomainException
{
    private const int CODE = 10;

    public UnexpectedNullSortingField()
        : base(CODE, "The sorting field must not be null.") { }
}
