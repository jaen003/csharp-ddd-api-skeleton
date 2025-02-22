using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;

namespace Src.Core.Restaurants.Application;

public interface IRestaurantRepository
{
    Task<bool> ExistsByStatusNotAndId(RestaurantStatus status, Guid id);

    Task Save(Restaurant restaurant);
}
