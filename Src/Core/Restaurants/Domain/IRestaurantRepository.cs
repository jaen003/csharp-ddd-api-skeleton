using Src.Core.Restaurants.Domain.Aggregates;
using Src.Core.Restaurants.Domain.ValueObjects;
using Src.Core.Shared.Domain.ValueObjects;

namespace Src.Core.Restaurants.Domain;

public interface IRestaurantRepository
{
    Task<bool> ExistsByStatusNotAndId(RestaurantStatus status, Uuid id);

    Task Save(Restaurant restaurant);
}
