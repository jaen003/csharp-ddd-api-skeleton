using Src.Core.Restaurants.Domain.Exceptions;
using Src.Core.Restaurants.Domain.ValueObjects;

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
        _ = new RestaurantStatus(value);
    }

    [Fact]
    public void IsInvalid()
    {
        Assert.Throws<InvalidRestaurantStatusException>(() => new RestaurantStatus(2));
    }
}
