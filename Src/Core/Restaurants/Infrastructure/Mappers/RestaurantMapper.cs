using Riok.Mapperly.Abstractions;
using Src.Core.Restaurants.Infrastructure.Models;
using Src.Core.Restaurants.Domain.Aggregates;

namespace Src.Core.Restaurants.Infrastructure.Mappers;

[Mapper]
public partial class RestaurantMapper
{
    public partial RestaurantModel ToModel(Restaurant restaurant);
}
