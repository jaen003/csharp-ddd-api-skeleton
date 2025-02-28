namespace Src.Core.Shared.Domain.ValueObjects;

public class NonNegativeInt : NonNegativeNumber<int>
{
    private const int MINIMUM_VALUE = 0;

    public NonNegativeInt(int value, string valueObjectName, string aggregateName)
        : base(value, valueObjectName, aggregateName) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MINIMUM_VALUE);
    }
}
