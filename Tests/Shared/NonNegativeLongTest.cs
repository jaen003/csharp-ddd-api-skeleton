using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonNegativeLongTest
{
    [Fact]
    public void IsEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        NonNegativeLong otherValueObject = new(3, "count", "test");
        Assert.True(valueObject.Equals(otherValueObject));
        Assert.True(valueObject == otherValueObject);
        Assert.True(valueObject.Equals(3));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(long value, long otherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        NonNegativeLong otherValueObject = new(otherValue, "count", "test");
        Assert.False(valueObject.Equals(otherValueObject));
        Assert.False(valueObject == otherValueObject);
        Assert.False(valueObject.Equals(otherValue));
    }

    [Fact]
    public void IsLessThan()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        NonNegativeLong otherValueObject = new(4, "count", "test");
        Assert.True(valueObject.IsLessThan(otherValueObject));
        Assert.True(valueObject < otherValueObject);
        Assert.True(valueObject.IsLessThan(4));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(long value, long otherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        NonNegativeLong otherValueObject = new(otherValue, "count", "test");
        Assert.False(valueObject.IsLessThan(otherValueObject));
        Assert.False(valueObject < otherValueObject);
        Assert.False(valueObject.IsLessThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(long value, long otherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        NonNegativeLong otherValueObject = new(otherValue, "count", "test");
        Assert.True(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.True(valueObject <= otherValueObject);
        Assert.True(valueObject.IsLessThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        NonNegativeLong otherValueObject = new(2, "count", "test");
        Assert.False(valueObject.IsLessThanOrEqual(otherValueObject));
        Assert.False(valueObject <= otherValueObject);
        Assert.False(valueObject.IsLessThanOrEqual(2));
    }

    [Fact]
    public void IsGreaterThan()
    {
        NonNegativeLong valueObject = new(4, "count", "test");
        NonNegativeLong otherValueObject = new(3, "count", "test");
        Assert.True(valueObject.IsGreaterThan(otherValueObject));
        Assert.True(valueObject > otherValueObject);
        Assert.True(valueObject.IsGreaterThan(3));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(long value, long otherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        NonNegativeLong otherValueObject = new(otherValue, "count", "test");
        Assert.False(valueObject.IsGreaterThan(otherValueObject));
        Assert.False(valueObject > otherValueObject);
        Assert.False(valueObject.IsGreaterThan(otherValue));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(long value, long otherValue)
    {
        NonNegativeLong valueObject = new(value, "count", "test");
        NonNegativeLong otherValueObject = new(otherValue, "count", "test");
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.True(valueObject >= otherValueObject);
        Assert.True(valueObject.IsGreaterThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        NonNegativeLong valueObject = new(3, "count", "test");
        NonNegativeLong otherValueObject = new(4, "count", "test");
        Assert.False(valueObject.IsGreaterThanOrEqual(otherValueObject));
        Assert.False(valueObject.IsGreaterThanOrEqual(4));
        Assert.False(valueObject >= otherValueObject);
    }

    [Fact]
    public void IsNegative()
    {
        Assert.Throws<NegativeNumberNotAllowedException<long>>(
            () => new NonNegativeLong(-1, "count", "test")
        );
    }
}
