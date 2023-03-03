using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductName : NonEmptyStringValueObject
{
    public ProductName(string value)
        : base(value) { }
}
