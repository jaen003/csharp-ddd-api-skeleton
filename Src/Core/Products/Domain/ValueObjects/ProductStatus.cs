using Src.Core.Products.Domain.Exceptions;
using Src.Core.Shared.Domain.Aggregates;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Products.Domain.ValueObjects;

public record ProductStatus : NonNegativeShort
{
    private const short Active = 0;
    public const short Deleted = 1;
    private const string ValueObjectName = "status";

    public ProductStatus(short value)
        : base(value, ValueObjectName, AggregateName.Product)
    {
        if (!IsValid())
        {
            throw new InvalidProductStatusException(value);
        }
    }

    public static ProductStatus CreateActive()
    {
        return new ProductStatus(Active);
    }

    public static ProductStatus CreateDeleted()
    {
        return new ProductStatus(Deleted);
    }

    public bool IsActive()
    {
        return Equals(Active);
    }

    private bool IsValid()
    {
        return Equals(Active) || Equals(Deleted);
    }
}
