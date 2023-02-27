using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeIntValueObjectTest
{
    [Fact]
    public void IsNegative()
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeIntValueObject valueObject = new(-1);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(4, exceptionCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsNotNegative(int value)
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeIntValueObject valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
