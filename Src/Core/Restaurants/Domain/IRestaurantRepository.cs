using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain;

public interface IRestaurantRepository
{
    Task<bool> ExistsByStatusNotAndId(RestaurantStatus status, RestaurantId id);

    Task Save(Restaurant restaurant);
}
