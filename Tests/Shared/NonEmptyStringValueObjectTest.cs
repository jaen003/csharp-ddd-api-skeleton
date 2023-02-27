using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.ValueObjects;

namespace Tests.Shared;

public class NonEmptyStringValueObjectTest
{
    [Fact]
    public void IsEmpty()
    {
        int exceptionCode = 0;
        try
        {
            NonEmptyStringValueObject valueObject = new("");
        }
        catch (DomainException exception)
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
            NonEmptyStringValueObject valueObject = new("H");
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }
}
