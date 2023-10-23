using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeIntTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeInt valueObject = new(3);
        Assert.True(valueObject.Equals(new NonNegativeInt(3)));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value);
        Assert.False(valueObject.Equals(new NonNegativeInt(anotherValue)));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeInt valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new NonNegativeInt(4)));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new NonNegativeInt(anotherValue)));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new NonNegativeInt(anotherValue)));
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeInt valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeInt(2)));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeInt valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new NonNegativeInt(3)));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new NonNegativeInt(anotherValue)));
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new NonNegativeInt(anotherValue)));
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeInt valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeInt(4)));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        int exceptionCode = 0;
        try
        {
            NonNegativeInt valueObject = new(-1);
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
            NonNegativeInt valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
