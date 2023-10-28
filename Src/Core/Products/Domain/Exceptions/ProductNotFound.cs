using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNotFound : ValidationError
{
    private const int CODE = 202;

    public ProductNotFound(string id)
        : base(CODE, $"The product '{id}' has not been found.") { }
}
