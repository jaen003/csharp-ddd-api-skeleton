using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.Paginations.Exceptions;

public class NullPaginationSortingFieldNotAllowedException : CustomDomainException
{
    public NullPaginationSortingFieldNotAllowedException()
        : base(
            CustomExceptionCode.NullPaginationSortingFieldNotAllowed,
            "The pagination sorting field must not be null."
        ) { }
}
