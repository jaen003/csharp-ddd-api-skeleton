using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class EmailValueObjectTest
{
    [Theory]
    [InlineData("stephen.hawking@gmail.com")]
    [InlineData("stephen.hawking03@gmail.com")]
    [InlineData("stephen03@gmail.com")]
    [InlineData("stephen.hawking@oxford.edu.co")]
    public void IsValid(string value)
    {
        int exceptionCode = 0;
        try
        {
            EmailValueObject valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData("stephen.hawking@@gmail.com")]
    [InlineData("stephen.@gmail.com")]
    [InlineData("stephen03gmail.com")]
    [InlineData("stephen.hawking@oxford.edu.")]
    [InlineData("stephen.hawking@oxford.e")]
    public void IsInvalid(string value)
    {
        int exceptionCode = 0;
        try
        {
            EmailValueObject valueObject = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(6, exceptionCode);
    }
}
