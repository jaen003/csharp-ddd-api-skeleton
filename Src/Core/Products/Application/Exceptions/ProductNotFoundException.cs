using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Application.Exceptions;

public class ProductNotFoundException : CustomApplicationException
{
    public ProductNotFoundException(string id)
        : base(CustomExceptionCode.ProductNotFound, $"The product '{id}' has not been found.") { }
}
