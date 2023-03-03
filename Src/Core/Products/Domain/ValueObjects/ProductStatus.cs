using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public class ProductStatus : NonNegativeShortValueObject
{
    private const short ACTIVED = 1;
    private const short DELETED = 2;

    public ProductStatus(short value)
        : base(value) { }

    public static ProductStatus CreateActived()
    {
        return new ProductStatus(ACTIVED);
    }

    public static ProductStatus CreateDeleted()
    {
        return new ProductStatus(DELETED);
    }

    public bool IsActived()
    {
        return Equals(new ShortValueObject(ACTIVED));
    }
}
