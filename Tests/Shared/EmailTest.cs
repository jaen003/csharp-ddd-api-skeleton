using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class EmailTest
{
    [Theory]
    [InlineData("stephen.hawking@gmail.com")]
    [InlineData("stephen.hawking03@gmail.com")]
    [InlineData("stephen03@gmail.com")]
    [InlineData("stephen.hawking@oxford.edu.co")]
    public void IsValid(string value)
    {
        _ = new Email(value, "user");
    }

    [Theory]
    [InlineData("stephen.hawking@@gmail.com")]
    [InlineData("stephen.hawking@gmail..com")]
    [InlineData("stephen.hawking@gmail")]
    [InlineData("@gmail.com")]
    [InlineData("stephen.hawking@gmail..")]
    [InlineData("stephen.hawking@gmail com")]
    [InlineData("stephen.hawking@gmail,com")]
    [InlineData("stephen.hawking@gmail+.com")]
    [InlineData("stephen.hawking@gmail.com.")]
    [InlineData("stephen.hawking@gmail_com")]
    [InlineData("stephen.hawking@")]
    [InlineData("stephen.@gmail.com")]
    [InlineData("stephen03gmail.com")]
    [InlineData("stephen.hawking@oxford.edu.")]
    [InlineData("stephen.hawking@oxford.e")]
    public void IsInvalid(string value)
    {
        Assert.Throws<InvalidEmailFormatException>(() => new Email(value, "user"));
    }
}
