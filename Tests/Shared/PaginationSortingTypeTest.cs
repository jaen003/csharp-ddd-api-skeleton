using Src.Core.Shared.Domain.Exceptions;
using Src.Core.Shared.Domain.Paginations.ValueObjects;

namespace Tests.Shared;

public class PaginationSortingTypeTest
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
            PaginationSortingType valueObject = new(value);
        }
        catch (CustomException exception)
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
            PaginationSortingType valueObject = new(value);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(5, exceptionCode);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("desc")]
    public void IsDescending(string? value)
    {
        PaginationSortingType valueObject = new(value);
        Assert.True(valueObject.IsDescending());
    }
}
