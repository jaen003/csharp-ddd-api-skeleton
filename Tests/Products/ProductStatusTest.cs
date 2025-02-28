using Src.Core.Products.Domain.Exceptions;
using Src.Core.Products.Domain.ValueObjects;

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
        _ = new ProductStatus(value);
    }

    [Fact]
    public void IsInvalid()
    {
        Assert.Throws<InvalidProductStatusException>(() => new ProductStatus(2));
    }
}
