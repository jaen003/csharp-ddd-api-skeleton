using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class IntValueObjectTest
{
    [Fact]
    public void IsEqual()
    {
        IntValueObject valueObject = new(3);
        Assert.True(valueObject.Equals(new IntValueObject(3)));
    }

    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 4)]
    public void IsNotEqual(int value, int anotherValue)
    {
        IntValueObject valueObject = new(value);
        Assert.False(valueObject.Equals(new IntValueObject(anotherValue)));
    }

    [Fact]
    public void IsLessThan()
    {
        IntValueObject valueObject = new(3);
        Assert.True(valueObject.IsLessThan(new IntValueObject(4)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 2)]
    public void IsNotLessThan(int value, int anotherValue)
    {
        IntValueObject valueObject = new(value);
        Assert.False(valueObject.IsLessThan(new IntValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsLessThanOrEqual(int value, int anotherValue)
    {
        IntValueObject valueObject = new(value);
        Assert.True(valueObject.IsLessThanOrEqual(new IntValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotLessThanOrEqual()
    {
        IntValueObject valueObject = new(3);
        Assert.False(valueObject.IsLessThanOrEqual(new IntValueObject(2)));
    }

    [Fact]
    public void IsGreaterThan()
    {
        IntValueObject valueObject = new(4);
        Assert.True(valueObject.IsGreaterThan(new IntValueObject(3)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    public void IsNotGreaterThan(int value, int anotherValue)
    {
        IntValueObject valueObject = new(value);
        Assert.False(valueObject.IsGreaterThan(new IntValueObject(anotherValue)));
    }

    [Theory]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    public void IsGreaterThanOrEqual(int value, int anotherValue)
    {
        IntValueObject valueObject = new(value);
        Assert.True(valueObject.IsGreaterThanOrEqual(new IntValueObject(anotherValue)));
    }

    [Fact]
    public void IsNotGreaterThanOrEqual()
    {
        IntValueObject valueObject = new(3);
        Assert.False(valueObject.IsGreaterThanOrEqual(new IntValueObject(4)));
    }
}
