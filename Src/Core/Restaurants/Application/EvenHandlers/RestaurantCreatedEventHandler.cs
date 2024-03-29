using Src.Core.Restaurants.Application.Services;
using Src.Core.Restaurants.Domain.Events;
using Src.Core.Shared.Domain.Events;

namespace Src.Core.Restaurants.Application.EvenHandlers;

public class RestaurantCreatedEventHandler : IDomainEventHandler<RestaurantCreated>
{
    private readonly RestaurantCreator creator;

    public RestaurantCreatedEventHandler(RestaurantCreator creator)
    {
        this.creator = creator;
    }

    public async Task Handle(RestaurantCreated _event)
    {
        await creator.Create(_event.Id, _event.Name);
    }
}
