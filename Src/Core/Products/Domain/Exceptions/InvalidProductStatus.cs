using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class InvalidProductStatus : DomainException
{
    public InvalidProductStatus(short status)
        : base(
            CustomExceptionCode.InvalidProductStatus,
            $"The product status '{status}' is invalid."
        ) { }
}
