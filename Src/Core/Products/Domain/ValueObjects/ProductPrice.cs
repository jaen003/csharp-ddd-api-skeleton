using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductPrice : NonNegativeInt
{
    private const string VALUE_OBJECT_NAME = "price";

    public ProductPrice(int value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PRODUCT) { }
}
