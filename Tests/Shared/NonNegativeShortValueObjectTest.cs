using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeShortValueObjectTest
{
    [Fact]
    public void IsNegative()
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeShortValueObject valueObject = new(-1);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(12, exceptionCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsNotNegative(short value)
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeShortValueObject valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
