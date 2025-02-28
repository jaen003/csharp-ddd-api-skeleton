using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeLongTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        Assert.True(valueObject.Equals(new NonNegativeLong(3, "count", "test")));
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        Assert.False(valueObject.Equals(new NonNegativeLong(anotherValue, "count", "test")));
        Assert.False(valueObject.Equals(anotherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        Assert.True(valueObject.IsLessThan(new NonNegativeLong(4, "count", "test")));
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        Assert.False(valueObject.IsLessThan(new NonNegativeLong(anotherValue, "count", "test")));
        Assert.False(valueObject.IsLessThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        Assert.True(
            valueObject.IsLessThanOrEqual(new NonNegativeLong(anotherValue, "count", "test"))
        );
        Assert.True(valueObject.IsLessThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        Assert.False(valueObject.IsLessThanOrEqual(new NonNegativeLong(2, "count", "test")));
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeLong valueObject = new(4, "count", "test");
        Assert.True(valueObject.IsGreaterThan(new NonNegativeLong(3, "count", "test")));
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        Assert.False(valueObject.IsGreaterThan(new NonNegativeLong(anotherValue, "count", "test")));
        Assert.False(valueObject.IsGreaterThan(anotherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(long value, long anotherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        Assert.True(
            valueObject.IsGreaterThanOrEqual(new NonNegativeLong(anotherValue, "count", "test"))
        );
        Assert.True(valueObject.IsGreaterThanOrEqual(anotherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(new NonNegativeLong(4, "count", "test")));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<long>>(
            () => new NonNegativeLong(-1, "count", "test")
        );
    }
}
