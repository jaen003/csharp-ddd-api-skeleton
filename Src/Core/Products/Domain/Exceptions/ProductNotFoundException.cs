using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNotFoundException : ValidationErrorException
{
    private const int CODE = 202;

    public ProductNotFoundException(int code, string message)
        : base(code, message) { }

    public ProductNotFoundException() { }

    public ProductNotFoundException(long id)
        : base(CODE, $"The product '{id}' has not been found.") { }

    public ProductNotFoundException(string? message)
        : base(message) { }

    public ProductNotFoundException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public ProductNotFoundException(int code, int type, string message)
        : base(code, type, message) { }
}
