using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Paginations;

namespace Tests.Shared;

public class SortingTypeTest
{
    [Theory]
    [InlineData("asc")]
    [InlineData("desc")]
    [InlineData(null)]
    public void IsValid(string? value)
    {
        int exceptionCode = 0;
        try
        {
            SortingType sortingType = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData("ASC")]
    [InlineData("DESC")]
    [InlineData("des")]
    public void IsInvalid(string? value)
    {
        int exceptionCode = 0;
        try
        {
            SortingType sortingType = new(value);
        }
        catch (DomainException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(9, exceptionCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("desc")]
    public void IsDescending(string? value)
    {
        SortingType sortingType = new(value);
        Assert.True(sortingType.IsDescending());
    }
}
