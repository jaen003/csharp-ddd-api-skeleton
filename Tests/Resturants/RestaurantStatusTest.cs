using Src.Core.Restaurants.Domain.ValueObjects;

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
}
