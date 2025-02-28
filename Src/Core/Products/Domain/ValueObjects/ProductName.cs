using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductName : NonEmptyString
{
    private const string VALUE_OBJECT_NAME = "name";

    public ProductName(string value)
        : base(value, VALUE_OBJECT_NAME, AggregateName.PRODUCT) { }
}
