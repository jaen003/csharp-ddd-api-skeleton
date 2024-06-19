using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.Exceptions;

namespace Tests.Resturants;

public class RestaurantStatusTest
{
    [Fact]
    public void IsActive()
    {
        RestaurantStatus valueObject = RestaurantStatus.CreateActive();
        Assert.True(valueObject.IsActive());
        Assert.Equal(0, valueObject.Value);
    }

    [Fact]
    public void IsDeleted()
    {
        RestaurantStatus valueObject = RestaurantStatus.CreateDeleted();
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
            RestaurantStatus valueObject = new(value);
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
            RestaurantStatus valueObject = new(value);
        }
        catch (CustomException exception)
        {
            exceptionCode = exception.Code;
        }
        Assert.Equal(102, exceptionCode);
    }
}
