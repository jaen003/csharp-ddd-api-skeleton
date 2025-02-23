using Src.Core.Products.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductStatus : NonNegativeShort
{
    private const short ACTIVE = 0;
    public const short DELETED = 1;

    public ProductStatus(short value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidProductStatusException(value);
        }
    }

    public static ProductStatus CreateActive()
    {
        return new ProductStatus(ACTIVE);
    }

    public static ProductStatus CreateDeleted()
    {
        return new ProductStatus(DELETED);
    }

    public bool IsActive()
    {
        return Equals(ACTIVE);
    }

    private bool IsValid()
    {
        return Equals(ACTIVE) || Equals(DELETED);
    }
}
