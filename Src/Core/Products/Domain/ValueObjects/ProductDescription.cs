using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductDescription : NonEmptyStringValueObject
{
    public ProductDescription(string value)
        : base(value) { }
}
