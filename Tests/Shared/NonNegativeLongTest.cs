using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeLongTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeLong valueObject = new(3);
        Assert.True(valueObject.Equals(new NonNegativeLong(3)));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value);
        Assert.False(valueObject.Equals(new NonNegativeLong(anotherValue)));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeLong valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new NonNegativeLong(4)));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new NonNegativeLong(anotherValue)));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new NonNegativeLong(anotherValue)));
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeLong valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeLong(2)));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeLong valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new NonNegativeLong(3)));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new NonNegativeLong(anotherValue)));
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new NonNegativeLong(anotherValue)));
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeLong valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeLong(4)));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeLong valueObject = new(-1);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(11, exceptionCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsNotNegative(long value)
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeLong valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
