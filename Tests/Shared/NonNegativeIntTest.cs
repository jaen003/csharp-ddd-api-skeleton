using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeIntTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        Assert.True(valueObject.Equals(new NonNegativeInt(3, "price", "test")));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        Assert.False(valueObject.Equals(new NonNegativeInt(anotherValue, "price", "test")));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        Assert.True(valueObject.IsLessThan(new NonNegativeInt(4, "price", "test")));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        Assert.False(valueObject.IsLessThan(new NonNegativeInt(anotherValue, "price", "test")));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        Assert.True(
            valueObject.IsLessThanOrEqual(new NonNegativeInt(anotherValue, "price", "test"))
        );
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeInt(2, "price", "test")));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeInt valueObject = new(4, "price", "test");
        Assert.True(valueObject.IsGreaterThan(new NonNegativeInt(3, "price", "test")));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        Assert.False(valueObject.IsGreaterThan(new NonNegativeInt(anotherValue, "price", "test")));
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(int value, int anotherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        Assert.True(
            valueObject.IsGreaterThanOrEqual(new NonNegativeInt(anotherValue, "price", "test"))
        );
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeInt(4, "price", "test")));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<int>>(
            () => new NonNegativeInt(-1, "price", "test")
        );
    }
}
