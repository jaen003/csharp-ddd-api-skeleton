using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;
using Src.Core.Shared.Domain.Paginations;

namespace Tests.Shared;

public class PaginationLimitTest
{
    [Theory]
    [InlineData(29)]
    [InlineData(30)]
    public void IsValid(int value)
    {
        int exceptionCode = 0;
        try
        {
            PaginationLimit paginationLimit = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Fact]
    public void IsInvalid()
    {
        int exceptionCode = 0;
        try
        {
            PaginationLimit valueObject = new(31);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(7, exceptionCode);
    }
}
