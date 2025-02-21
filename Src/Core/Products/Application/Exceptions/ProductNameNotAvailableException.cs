using Src.Core.Shared.Application.Exceptions;
using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Application.Exceptions;

public class ProductNameNotAvailableException : CustomApplicationException
{
    public ProductNameNotAvailableException(string name)
        : base(
            CustomExceptionCode.ProductNameNotAvailable,
            $"The product name '{name}' is not available."
        ) { }
}
