using Src.Core.Shared.Domain.Paginations.Exceptions;
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
        _ = new PaginationSortingType(value);
    }

    [Theory]
    [InlineData("ASC")]
    [InlineData("DESC")]
    [InlineData("des")]
    public void IsInvalid(string? value)
    {
        Assert.Throws<InvalidPaginationSortingTypeException>(
            () => new PaginationSortingType(value)
        );
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
