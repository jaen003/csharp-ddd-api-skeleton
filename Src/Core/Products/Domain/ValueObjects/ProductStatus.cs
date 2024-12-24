using Src.Core.Products.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductStatus : NonNegativeShort
{
    private enum Type : short
    {
        Active,
        Deleted,
    }

    public ProductStatus(short value)
        : base(value)
    {
        if (!IsValid())
        {
            throw new InvalidProductStatus(value);
        }
    }

    public static ProductStatus CreateActive()
    {
        return new ProductStatus((short)Type.Active);
    }

    public static ProductStatus CreateDeleted()
    {
        return new ProductStatus((short)Type.Deleted);
    }

    public bool IsActive()
    {
        return Equals((short)Type.Active);
    }

    private bool IsValid()
    {
        return Equals((short)Type.Active) || Equals((short)Type.Deleted);
    }
}
