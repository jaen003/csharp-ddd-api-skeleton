using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductDescription : NonEmptyString
{
    private const string VALUE_OBJECT_NAME = "description";

    public ProductDescription(string value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PRODUCT) { }
}
