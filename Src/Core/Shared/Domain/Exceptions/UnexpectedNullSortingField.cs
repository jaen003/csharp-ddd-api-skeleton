namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNullSortingField : ValidationError
{
    private const int CODE = 10;

    public UnexpectedNullSortingField()
        : base(CODE, "The sorting field must not be null.") { }
}
