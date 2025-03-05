using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductDescription : NonEmptyString
{
    private const string ValueObjectName = "description";

    public ProductDescription(string value)
        : base(value, ValueObjectName, AggregateName.Product) { }
}
