using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNameNotAvailable : DomainException
{
    public ProductNameNotAvailable(string name)
        : base(
            CustomExceptionCode.ProductNameNotAvailable,
            $"The product name '{name}' is not available."
        ) { }
}
