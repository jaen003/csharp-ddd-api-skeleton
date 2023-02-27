using Src.Core.Shared.Domain.Exceptions;

namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeIntValueObject : IntValueObject
{
    private const int MINIMUN = 0;

    public NonNegativeIntValueObject(int value)
        : base(value)
    {
        if (IsNegative())
        {
            throw new NegativeIntException(value);
        }
    }

    public bool IsNegative()
    {
        return IsLessThan(new IntValueObject(MINIMUN));
    }
}
