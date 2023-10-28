using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeShortTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeShort valueObject = new(3);
        Assert.True(valueObject.Equals(new NonNegativeShort(3)));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value);
        Assert.False(valueObject.Equals(new NonNegativeShort(anotherValue)));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeShort valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new NonNegativeShort(4)));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new NonNegativeShort(anotherValue)));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new NonNegativeShort(anotherValue)));
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeShort valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeShort(2)));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeShort valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new NonNegativeShort(3)));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new NonNegativeShort(anotherValue)));
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new NonNegativeShort(anotherValue)));
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeShort valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeShort(4)));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeShort valueObject = new(-1);
        }
        catch (ApplicationException exception)
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
            NonNegativeShort valueObject = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
