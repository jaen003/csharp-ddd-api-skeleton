using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductPrice : NonNegativeInt
{
    private const string VALUE_OBJECT_NAME = "price";

    public ProductPrice(int value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PRODUCT) { }
}
