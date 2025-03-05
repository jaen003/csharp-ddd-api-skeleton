namespace Src.Core.Shared.Domain.ValueObjects;

public record NonNegativeInt : NonNegativeNumber<int>
{
    private const int MinimumValue = 0;

    public NonNegativeInt(int value, string valueObjectName, string aggregateName)
        : base(value, valueObjectName, aggregateName) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MinimumValue);
    }
}
