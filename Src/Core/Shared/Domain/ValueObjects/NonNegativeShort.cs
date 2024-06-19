namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeShort : NonNegativeNumber<short>
{
    private const short MINIMUM_VALUE = 0;

    public NonNegativeShort(short value)
        : base(value) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MINIMUM_VALUE);
    }
}
