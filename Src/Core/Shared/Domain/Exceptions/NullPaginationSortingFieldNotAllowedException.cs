namespace Src.Core.Shared.Domain.Exceptions;

public class NullPaginationSortingFieldNotAllowedException : CustomDomainException
{
    public NullPaginationSortingFieldNotAllowedException()
        : base(
            CustomExceptionCode.NullPaginationSortingFieldNotAllowed,
            "The field assigned to the pagination sorting must not be null."
        ) { }
}
