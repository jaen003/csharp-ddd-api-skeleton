namespace Src.Core.Restaurants.Application.Services;

public interface IRestaurantExistenceValidator
{
    Task Validate(Guid restaurantId);
}
