using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductName : NonEmptyString
{
    private const string ValueObjectName = "name";

    public ProductName(string value)
        : base(value, ValueObjectName, AggregateName.Product) { }
}
