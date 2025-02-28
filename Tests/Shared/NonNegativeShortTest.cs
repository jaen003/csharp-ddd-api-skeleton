using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeShortTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        Assert.True(valueObject.Equals(new NonNegativeShort(3, "status", "test")));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        Assert.False(valueObject.Equals(new NonNegativeShort(anotherValue, "status", "test")));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        Assert.True(valueObject.IsLessThan(new NonNegativeShort(4, "status", "test")));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        Assert.False(valueObject.IsLessThan(new NonNegativeShort(anotherValue, "status", "test")));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        Assert.True(
            valueObject.IsLessThanOrEqual(new NonNegativeShort(anotherValue, "status", "test"))
        );
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeShort(2, "status", "test")));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeShort valueObject = new(4, "status", "test");
        Assert.True(valueObject.IsGreaterThan(new NonNegativeShort(3, "status", "test")));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        Assert.False(
            valueObject.IsGreaterThan(new NonNegativeShort(anotherValue, "status", "test"))
        );
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(short value, short anotherValue)
    {
        NonNegativeShort valueObject = new(value, "status", "test");
        Assert.True(
            valueObject.IsGreaterThanOrEqual(new NonNegativeShort(anotherValue, "status", "test"))
        );
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeShort valueObject = new(3, "status", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeShort(4, "status", "test")));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<short>>(
            () => new NonNegativeShort(-1, "status", "test")
        );
    }
}
