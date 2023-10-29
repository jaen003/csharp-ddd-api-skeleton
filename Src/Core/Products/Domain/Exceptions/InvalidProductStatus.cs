using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class InvalidProductStatus : ValidationError
{
    private const int CODE = 203;

    public InvalidProductStatus(short status)
        : base(CODE, $"The product status '{status}' is invalid.") { }
}
