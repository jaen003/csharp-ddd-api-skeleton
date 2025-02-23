using Src.Core.Restaurants.Domain.Aggregates;

namespace Src.Core.Restaurants.Application;

public interface IRestaurantRepository
{
    Task<bool> ExistsByStatusNotAndId(short status, Guid id);

    Task Save(Restaurant restaurant);
}
