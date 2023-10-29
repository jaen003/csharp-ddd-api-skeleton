using Src.Core.Restaurants.Domain.ValueObjects;
using ApplicationException = Src.Core.Shared.Domain.Exceptions.ApplicationException;

namespace Tests.Resturants;

public class RestaurantStatusTest
{
    [Fact]
    public void IsActived()
    {
        RestaurantStatus valueObject = RestaurantStatus.CreateActived();
        Assert.True(valueObject.IsActived());
        Assert.Equal(1, valueObject.Value);
    }

    [Fact]
    public void IsDeleted()
    {
        RestaurantStatus valueObject = RestaurantStatus.CreateDeleted();
        Assert.Equal(2, valueObject.Value);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void IsValid(short value)
    {
        int exceptionCode = 0;
        try
        {
            RestaurantStatus valueObject = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(0, exceptionCode);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    public void IsInvalid(short value)
    {
        int exceptionCode = 0;
        try
        {
            RestaurantStatus valueObject = new(value);
        }
        catch (ApplicationException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(102, exceptionCode);
    }
}
