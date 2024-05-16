using Riok.Mapperly.Abstractions;
using RestaurantModel = Src.Core.Restaurants.Infrastructure.Database.Models.Restaurant;
using Src.Core.Restaurants.Domain.Aggregates;

namespace Src.Core.Restaurants.Infrastructure.Mappers;

[Mapper]
public partial class RestaurantMapper
{
    public partial RestaurantModel ToModel(Restaurant restaurant);
}
