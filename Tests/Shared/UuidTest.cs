using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class UuidTest
{
    [Theory]
    [InlineData("550e8400-e29b-41d4-a716-446655440000")]
    [InlineData("01234567-89ab-4cde-89ab-0123456789ab")]
    [InlineData("00000000-0000-4000-8000-000000000000")]
    [InlineData("b8199a4e-7b97-4ea2-b5f2-5802f7054429")]
    [InlineData("aaffffaa-5555-4a4a-aaaa-123456789abc")]
    public void IsValid(string value)
    {
        int exceptionCode = 0;
        try
        {
            Uuid valueObject = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData("not-a-uuid")]
    [InlineData("12345")]
    [InlineData("g54e8400-e29b-41d4-a716-446655440000")]
    [InlineData("01234567-89ab-4cde-89ab-0123456789a")]
    [InlineData("550e8400-e29b-41d4-a716-4466554400000")]
    [InlineData("550e8400-e29b-41d4-a716-44665544000")]
    [InlineData("550e8400-e29b-41d4-a716-44665544000!")]
    [InlineData("AABBCCDD-EF11-22GH-44II-5566778899JJ")]
    public void IsInvalid(string value)
    {
        int exceptionCode = 0;
        try
        {
            Uuid valueObject = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(13, exceptionCode);
    }
}
