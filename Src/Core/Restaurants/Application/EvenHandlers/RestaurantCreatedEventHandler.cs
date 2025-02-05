using Src.Core.Restaurants.Application.Dtos;
using Src.Core.Restaurants.Application.Services;
using Src.Core.Restaurants.Domain.Events;
using Src.Core.Shared.Application.EventHandlers;

namespace Src.Core.Restaurants.Application.EvenHandlers;

public class RestaurantCreatedEventHandler : IDomainEventHandler<RestaurantCreatedDomainEvent>
{
    private readonly RestaurantCreator creator;

    public RestaurantCreatedEventHandler(RestaurantCreator creator)
    {
        this.creator = creator;
    }

    public async Task Handle(RestaurantCreatedDomainEvent domainEvent)
    {
        RestaurantCreationDto creationDto = new(domainEvent.Id, domainEvent.Name);
        await creator.Create(creationDto);
    }
}
