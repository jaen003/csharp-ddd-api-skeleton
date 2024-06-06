using Src.Core.Products.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;

namespace Tests.Products;

public class ProductStatusTest
{
    [Fact]
    public void IsActive()
    {
        ProductStatus valueObject = ProductStatus.CreateActive();
        Assert.True(valueObject.IsActive());
        Assert.Equal(0, valueObject.Value);
    }

    [Fact]
    public void IsDeleted()
    {
        ProductStatus valueObject = ProductStatus.CreateDeleted();
        Assert.Equal(1, valueObject.Value);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    public void IsValid(short value)
    {
        int exceptionCode = 0;
        try
        {
            ProductStatus valueObject = new(value);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData(2)]
    public void IsInvalid(short value)
    {
        int exceptionCode = 0;
        try
        {
            ProductStatus valueObject = new(value);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(203, exceptionCode);
    }
}
