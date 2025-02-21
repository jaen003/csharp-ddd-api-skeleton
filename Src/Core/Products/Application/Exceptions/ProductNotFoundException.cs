using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Application.Exceptions;

public class ProductNotFoundException : DomainException
{
    public ProductNotFoundException(string id)
        : base(CustomExceptionCode.ProductNotFound, $"The product '{id}' has not been found.") { }
}
