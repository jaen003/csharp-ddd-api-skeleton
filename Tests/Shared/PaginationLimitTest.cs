using Src.Core.Shared.Domain.Paginations.Exceptions;
using Src.Core.Shared.Domain.Paginations.ValueObjects;

namespace Tests.Shared;

public class PaginationLimitTest
{
    [Theory]
    [InlineData(0)]
    [InlineData(30)]
    public void IsValid(short value)
    {
        _ = new PaginationLimit(value);
    }

    [Fact]
    public void IsInvalid()
    {
        Assert.Throws<InvalidPaginationLimitException>(() => new PaginationLimit(31));
    }
}
