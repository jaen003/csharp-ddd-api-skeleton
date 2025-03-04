namespace Src.Core.Shared.Domain.ValueObjects;

public record NonNegativeShort : NonNegativeNumber<short>
{
    private const short MINIMUM_VALUE = 0;

    public NonNegativeShort(short value, string valueObjectName, string aggregateName)
        : base(value, valueObjectName, aggregateName) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MINIMUM_VALUE);
    }
}
