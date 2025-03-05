using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductPrice : NonNegativeInt
{
    private const string ValueObjectName = "price";

    public ProductPrice(int value)
        : base(value, ValueObjectName, AggregateName.Product) { }
}
