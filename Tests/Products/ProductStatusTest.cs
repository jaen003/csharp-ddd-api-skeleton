using Src.Core.Products.Domain.ValueObjects;

namespace Tests.Products;

public class ProductStatusTest
{
    [Fact]
    public void IsActived()
    {
        ProductStatus valueObject = ProductStatus.CreateActived();
        Assert.True(valueObject.IsActived());
        Assert.Equal(1, valueObject.Value);
    }

    [Fact]
    public void IsDeleted()
    {
        ProductStatus valueObject = ProductStatus.CreateDeleted();
        Assert.Equal(2, valueObject.Value);
    }
}
