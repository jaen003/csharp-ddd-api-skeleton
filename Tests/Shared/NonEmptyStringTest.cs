using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonEmptyStringTest
{
    [Fact]
    public void IsEqual()
    {
        NonEmptyString valueObject = new("Hello world!");
        Assert.True(valueObject.Equals(new NonEmptyString("Hello world!")));
        Assert.True(valueObject.Equals("Hello world!"));
    }

    [Fact]
    public void IsNotEqual()
    {
        NonEmptyString valueObject = new("Hello world");
        Assert.False(valueObject.Equals(new NonEmptyString("Hello world!")));
        Assert.False(valueObject.Equals("Hello world!"));
    }

    [Fact]
    public void IsEmpty()
    {
        int exceptionCode = 0;
        try
        {
            NonEmptyString valueObject = new("");
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(5, exceptionCode);
    }

    [Fact]
    public void IsNotEmpty()
    {
        int exceptionCode = 0;
        try
        {
            NonEmptyString valueObject = new("H");
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData("Hello world!", "(.*)world!")]
    [InlineData("Hello world!", "Hello")]
    [InlineData("Hello world!", "(.*)world(.*)")]
    [InlineData("Hello world!", "world")]
    public void IsMatch(string value, string anotherValue)
    {
        NonEmptyString valueObject = new(value);
        Assert.True(valueObject.Matches(new NonEmptyString(anotherValue)));
        Assert.True(valueObject.Matches(anotherValue));
    }

    [Theory]
    [InlineData("Hello world!", "World")]
    [InlineData("Hello world!", "hello")]
    [InlineData("Hello world!", "(.*)World!")]
    [InlineData("Hello world!", "(.*)World(.*)")]
    public void IsNotMatch(string value, string anotherValue)
    {
        NonEmptyString valueObject = new(value);
        Assert.False(valueObject.Matches(new NonEmptyString(anotherValue)));
        Assert.False(valueObject.Matches(anotherValue));
    }

    [Fact]
    public void IsLongerThan()
    {
        NonEmptyString valueObject = new("Hello world!");
        Assert.True(valueObject.IsLongerThan(new NonEmptyString("Hello world")));
        Assert.True(valueObject.IsLongerThan("Hello world"));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world", "Hello world!")]
    public void IsNotLongerThan(string value, string anotherValue)
    {
        NonEmptyString valueObject = new(value);
        Assert.False(valueObject.IsLongerThan(new NonEmptyString(anotherValue)));
        Assert.False(valueObject.IsLongerThan(anotherValue));
    }

    [Theory]
    [InlineData("Hello world!", "Hello world!")]
    [InlineData("Hello world!", "Hello world")]
    public void IsLongerThanOrEqual(string value, string anotherValue)
    {
        NonEmptyString valueObject = new(value);
        Assert.True(valueObject.IsLongerThanOrEqual(new NonEmptyString(anotherValue)));
        Assert.True(valueObject.IsLongerThanOrEqual(anotherValue));
    }

    [Fact]
    public void TestIsNotLongerThanOrEqual()
    {
        NonEmptyString valueObject = new("Hello world");
        Assert.False(valueObject.IsLongerThanOrEqual(new NonEmptyString("Hello world!")));
        Assert.False(valueObject.IsLongerThanOrEqual("Hello world!"));
    }
}
