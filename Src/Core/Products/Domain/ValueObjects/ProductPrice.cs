using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductPrice : NonNegativeIntValueObject
{
    public ProductPrice(int value)
        : base(value) { }
}
