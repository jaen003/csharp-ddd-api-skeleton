using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class ShortValueObjectTest
{
    [Fact]
    public void IsEqual()
    {
        ShortValueObject valueObject = new(3);
        Assert.True(valueObject.Equals(new ShortValueObject(3)));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(short value, short anotherValue)
    {
        ShortValueObject valueObject = new(value);
        Assert.False(valueObject.Equals(new ShortValueObject(anotherValue)));
    }

    [Fact]
    public void IsLessThan()
    {
        ShortValueObject valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new ShortValueObject(4)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(short value, short anotherValue)
    {
        ShortValueObject valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new ShortValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(short value, short anotherValue)
    {
        ShortValueObject valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new ShortValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        ShortValueObject valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new ShortValueObject(2)));
    }

    [Fact]
    public void IsGreaterThan()
    {
        ShortValueObject valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new ShortValueObject(3)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(short value, short anotherValue)
    {
        ShortValueObject valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new ShortValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(short value, short anotherValue)
    {
        ShortValueObject valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new ShortValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        ShortValueObject valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new ShortValueObject(4)));
    }
}
