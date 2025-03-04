using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeShortTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        NonNegativeShort otherValueObject = new(3, "status", "test");
        Assert.True(valueObject.Equals(otherValueObject));
        Assert.True(valueObject == otherValueObject);
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(short value, short otherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        NonNegativeShort otherValueObject = new(otherValue, "status", "test");
        Assert.False(valueObject.Equals(otherValueObject));
        Assert.False(valueObject == otherValueObject);
        Assert.False(valueObject.Equals(otherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        NonNegativeShort otherValueObject = new(4, "status", "test");
        Assert.True(valueObject.IsLessThan(otherValueObject));
        Assert.True(valueObject < otherValueObject);
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(short value, short otherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        NonNegativeShort otherValueObject = new(otherValue, "status", "test");
        Assert.False(valueObject.IsLessThan(otherValueObject));
        Assert.False(valueObject < otherValueObject);
        Assert.False(valueObject.IsLessThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(short value, short otherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        NonNegativeShort otherValueObject = new(otherValue, "status", "test");
        Assert.True(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.True(valueObject <= otherValueObject);
        Assert.True(valueObject.IsLessThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        NonNegativeShort otherValueObject = new(2, "status", "test");
        Assert.False(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.False(valueObject <= otherValueObject);
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeShort valueObject = new(4, "status", "test");
        NonNegativeShort otherValueObject = new(3, "status", "test");
        Assert.True(valueObject.IsGreaterThan(otherValueObject));
        Assert.True(valueObject > otherValueObject);
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(short value, short otherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        NonNegativeShort otherValueObject = new(otherValue, "status", "test");
        Assert.False(valueObject.IsGreaterThan(otherValueObject));
        Assert.False(valueObject > otherValueObject);
        Assert.False(valueObject.IsGreaterThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(short value, short otherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        NonNegativeShort otherValueObject = new(otherValue, "status", "test");
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.True(valueObject >= otherValueObject);
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        NonNegativeShort otherValueObject = new(4, "status", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
        Assert.False(valueObject >= otherValueObject);
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<short>>(
            () => new NonNegativeShort(-1, "status", "test")
        );
    }
}
