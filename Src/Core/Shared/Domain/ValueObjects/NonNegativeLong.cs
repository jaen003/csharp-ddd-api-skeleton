namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeLong : NonNegativeNumber<long>
{
    private const long MINIMUM_VALUE = 0;

    public NonNegativeLong(long value)
        : base(value) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MINIMUM_VALUE);
    }
}
