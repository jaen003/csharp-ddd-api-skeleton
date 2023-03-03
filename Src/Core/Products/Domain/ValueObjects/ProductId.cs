using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductId : NonNegativeLongValueObject
{
    public ProductId(long value)
        : base(value) { }
}
