using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNotFound : DomainException
{
    public ProductNotFound(string id)
        : base(CustomExceptionCode.ProductNotFound, $"The product '{id}' has not been found.") { }
}
