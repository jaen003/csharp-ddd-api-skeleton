namespace Src.Core.Shared.Domain.ValueObjects;

public record NonNegativeLong : NonNegativeNumber<long>
{
    private const long MinimumValue = 0;

    public NonNegativeLong(long value, string valueObjectName, string aggregateName)
        : base(value, valueObjectName, aggregateName) { }

    protected override bool IsNegative()
    {
        return IsLessThan(MinimumValue);
    }
}
