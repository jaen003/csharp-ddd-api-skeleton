using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Products.Domain.Exceptions;

public class ProductNameAlreadyCreatedException : ValidationErrorException
{
    private const int CODE = 201;

    public ProductNameAlreadyCreatedException(int code, string message)
        : base(code, message) { }

    public ProductNameAlreadyCreatedException() { }

    public ProductNameAlreadyCreatedException(string name)
        : base(CODE, $"The product name '{name}' has already been created.") { }

    public ProductNameAlreadyCreatedException(string? message, Exception? innerException)
        : base(message, innerException) { }

    public ProductNameAlreadyCreatedException(int code, int type, string message)
        : base(code, type, message) { }
}
