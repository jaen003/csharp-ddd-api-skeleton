using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeIntTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        NonNegativeInt otherValueObject = new(3, "price", "test");
        Assert.True(valueObject.Equals(otherValueObject));
        Assert.True(valueObject == otherValueObject);
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(int value, int otherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        NonNegativeInt otherValueObject = new(otherValue, "price", "test");
        Assert.False(valueObject.Equals(otherValueObject));
        Assert.False(valueObject == otherValueObject);
        Assert.False(valueObject.Equals(otherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        NonNegativeInt otherValueObject = new(4, "price", "test");
        Assert.True(valueObject.IsLessThan(otherValueObject));
        Assert.True(valueObject < otherValueObject);
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(int value, int otherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        NonNegativeInt otherValueObject = new(otherValue, "price", "test");
        Assert.False(valueObject.IsLessThan(otherValueObject));
        Assert.False(valueObject < otherValueObject);
        Assert.False(valueObject.IsLessThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(int value, int otherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        NonNegativeInt otherValueObject = new(otherValue, "price", "test");
        Assert.True(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.True(valueObject <= otherValueObject);
        Assert.True(valueObject.IsLessThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        NonNegativeInt otherValueObject = new(2, "price", "test");
        Assert.False(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.False(valueObject <= otherValueObject);
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeInt valueObject = new(4, "price", "test");
        NonNegativeInt otherValueObject = new(3, "price", "test");
        Assert.True(valueObject.IsGreaterThan(otherValueObject));
        Assert.True(valueObject > otherValueObject);
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(int value, int otherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        NonNegativeInt otherValueObject = new(otherValue, "price", "test");
        Assert.False(valueObject.IsGreaterThan(otherValueObject));
        Assert.False(valueObject > otherValueObject);
        Assert.False(valueObject.IsGreaterThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(int value, int otherValue)
    {
        NonNegativeInt valueObject = new(value, "price", "test");
        NonNegativeInt otherValueObject = new(otherValue, "price", "test");
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.True(valueObject >= otherValueObject);
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeInt valueObject = new(3, "price", "test");
        NonNegativeInt otherValueObject = new(4, "price", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
        Assert.False(valueObject >= otherValueObject);
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<int>>(
            () => new NonNegativeInt(-1, "price", "test")
        );
    }
}
