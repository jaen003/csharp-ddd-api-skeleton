using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class StringValueObjectTest
{
    [Fact]
    public void IsEqual()
    {
        StringValueObject valueObject = new("Hello world!");
        Assert.True(valueObject.Equals(new StringValueObject("Hello world!")));
    }

    [Fact]
    public void IsNotEqual()
    {
        StringValueObject valueObject = new("Hello world");
        Assert.False(valueObject.Equals(new StringValueObject("Hello world!")));
    }

    [Fact]
    public void IsEmpty()
    {
        StringValueObject valueObject = new("");
        Assert.True(valueObject.IsEmpty());
    }

    [Fact]
    public void IsNotEmpty()
    {
        StringValueObject valueObject = new("H");
        Assert.False(valueObject.IsEmpty());
    }

    [Fact]
    public void IsLongerThan()
    {
        StringValueObject valueObject = new("Hello world!");
        Assert.True(valueObject.IsLongerThan(new StringValueObject("Hello world")));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world", "Hello world!")]
    public void IsNotLongerThan(string value, string anotherValue)
    {
        StringValueObject valueObject = new(value);
        Assert.False(valueObject.IsLongerThan(new StringValueObject(anotherValue)));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world!", "Hello world")]
    public void IsLongerThanOrEqual(string value, string anotherValue)
    {
        StringValueObject valueObject = new(value);
        Assert.True(valueObject.IsLongerThanOrEqual(new StringValueObject(anotherValue)));
    }

    [Fact]
    public void TestIsNotLongerThanOrEqual()
    {
        StringValueObject valueObject = new("Hello world");
        Assert.False(valueObject.IsLongerThanOrEqual(new StringValueObject("Hello world!")));
    }

    [Theory]
    [InlineData("Hello world!", "(.*)world!")]
    [InlineData("Hello world!", "Hello")]
    [InlineData("Hello world!", "(.*)world(.*)")]
    [InlineData("Hello world!", "world")]
    public void IsMatch(string value, string anotherValue)
    {
        StringValueObject valueObject = new(value);
        Assert.True(valueObject.Matches(new StringValueObject(anotherValue)));
    }

    [Theory]
    [InlineData("Hello world!", "World")]
    [InlineData("Hello world!", "hello")]
    [InlineData("Hello world!", "(.*)World!")]
    [InlineData("Hello world!", "(.*)World(.*)")]
    public void IsNotMatch(string value, string anotherValue)
    {
        StringValueObject valueObject = new(value);
        Assert.False(valueObject.Matches(new StringValueObject(anotherValue)));
    }
}
