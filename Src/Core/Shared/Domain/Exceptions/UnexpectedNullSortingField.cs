namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNullSortingField : DomainException
{
    public UnexpectedNullSortingField()
        : base(
            CustomExceptionCode.UnexpectedNullSortingField,
            "The sorting field must not be null."
        ) { }
}
