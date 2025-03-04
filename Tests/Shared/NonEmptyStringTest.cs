using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonEmptyStringTest
{
    [Fact]
    public void IsEqual()
    {
        NonEmptyString valueObject = new("Hello world!", "message", "test");
        NonEmptyString otherValueObject = new("Hello world!", "message", "test");
        Assert.True(valueObject.Equals(otherValueObject));
        Assert.True(valueObject == otherValueObject);
        Assert.True(valueObject.Equals("Hello world!"));
    }

    [Fact]
    public void IsNotEqual()
    {
        NonEmptyString valueObject = new("Hello world", "message", "test");
        NonEmptyString otherValueObject = new("Hello world!", "message", "test");
        Assert.False(valueObject.Equals(otherValueObject));
        Assert.False(valueObject == otherValueObject);
        Assert.False(valueObject.Equals("Hello world!"));
    }

    [Fact]
    public void IsEmpty()
    {
        Assert.Throws<EmptyStringNotAllowedException>(
            () => new NonEmptyString("", "message", "test")
        );
    }

    [Theory]
    [InlineData("Hello world!", "(.*)world!")]
    [InlineData("Hello world!", "Hello")]
    [InlineData("Hello world!", "(.*)world(.*)")]
    [InlineData("Hello world!", "world")]
    public void IsMatch(string value, string otherValue)
    {
        NonEmptyString valueObject = new(value, "message", "test");
        Assert.True(valueObject.Matches(new NonEmptyString(otherValue, "message", "test")));
        Assert.True(valueObject.Matches(otherValue));
    }

    [Theory]
    [InlineData("Hello world!", "World")]
    [InlineData("Hello world!", "hello")]
    [InlineData("Hello world!", "(.*)World!")]
    [InlineData("Hello world!", "(.*)World(.*)")]
    public void IsNotMatch(string value, string otherValue)
    {
        NonEmptyString valueObject = new(value, "message", "test");
        Assert.False(valueObject.Matches(new NonEmptyString(otherValue, "message", "test")));
        Assert.False(valueObject.Matches(otherValue));
    }

    [Fact]
    public void IsLongerThan()
    {
        NonEmptyString valueObject = new("Hello world!", "message", "test");
        Assert.True(valueObject.IsLongerThan(new NonEmptyString("Hello world", "message", "test")));
        Assert.True(valueObject.IsLongerThan("Hello world"));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world", "Hello world!")]
    public void IsNotLongerThan(string value, string otherValue)
    {
        NonEmptyString valueObject = new(value, "message", "test");
        Assert.False(valueObject.IsLongerThan(new NonEmptyString(otherValue, "message", "test")));
        Assert.False(valueObject.IsLongerThan(otherValue));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world!", "Hello world")]
    public void IsLongerThanOrEqual(string value, string otherValue)
    {
        NonEmptyString valueObject = new(value, "message", "test");
        Assert.True(
            valueObject.IsLongerThanOrEqual(new NonEmptyString(otherValue, "message", "test"))
        );
        Assert.True(valueObject.IsLongerThanOrEqual(otherValue));
    }

    [Fact]
    public void IsNotLongerThanOrEqual()
    {
        NonEmptyString valueObject = new("Hello world", "message", "test");
        Assert.False(
            valueObject.IsLongerThanOrEqual(new NonEmptyString("Hello world!", "message", "test"))
        );
        Assert.False(valueObject.IsLongerThanOrEqual("Hello world!"));
    }
}
