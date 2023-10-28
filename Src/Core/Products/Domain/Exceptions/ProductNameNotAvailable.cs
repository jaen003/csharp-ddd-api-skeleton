using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNameNotAvailable : ValidationError
{
    private const int CODE = 201;

    public ProductNameNotAvailable(string name)
        : base(CODE, $"The product name '{name}' is not available.") { }
}
