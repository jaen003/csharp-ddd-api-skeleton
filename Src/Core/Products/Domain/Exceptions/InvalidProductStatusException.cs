using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class InvalidProductStatusException : DomainException
{
    public InvalidProductStatusException(short status)
        : base(
            CustomExceptionCode.InvalidProductStatus,
            $"The product status '{status}' is invalid."
        ) { }
}
