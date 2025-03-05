namespace Src.Core.Restaurants.Application.Validators;

public interface IRestaurantExistenceValidator
{
    Task Validate(Guid restaurantId);
}
