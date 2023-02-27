using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class LongValueObjectTest
{
    [Fact]
    public void IsEqual()
    {
        LongValueObject valueObject = new(3);
        Assert.True(valueObject.Equals(new LongValueObject(3)));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(long value, long anotherValue)
    {
        LongValueObject valueObject = new(value);
        Assert.False(valueObject.Equals(new LongValueObject(anotherValue)));
    }

    [Fact]
    public void IsLessThan()
    {
        LongValueObject valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new LongValueObject(4)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(long value, long anotherValue)
    {
        LongValueObject valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new LongValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(long value, long anotherValue)
    {
        LongValueObject valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new LongValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        LongValueObject valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new LongValueObject(2)));
    }

    [Fact]
    public void IsGreaterThan()
    {
        LongValueObject valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new LongValueObject(3)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(long value, long anotherValue)
    {
        LongValueObject valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new LongValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(long value, long anotherValue)
    {
        LongValueObject valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new LongValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        LongValueObject valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new LongValueObject(4)));
    }
}
