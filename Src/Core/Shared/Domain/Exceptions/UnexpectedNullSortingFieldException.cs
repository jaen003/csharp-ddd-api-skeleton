namespace Src.Core.Shared.Domain.Exceptions;

public class UnexpectedNullSortingFieldException : DomainException
{
    public UnexpectedNullSortingFieldException()
        : base(
            CustomExceptionCode.UnexpectedNullSortingField,
            "The sorting field must not be null."
        ) { }
}
