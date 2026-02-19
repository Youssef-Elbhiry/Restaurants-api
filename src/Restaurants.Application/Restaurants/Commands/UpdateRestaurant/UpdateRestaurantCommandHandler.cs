using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository,
    ILogger<UpdateRestaurantCommandHandler> logger,IRestaurantAuthorizationService restaurantauthorizationservice) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantFromDb = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);

        if (restaurantFromDb is  null)
            throw new NotFoundException(nameof(Restaurant), request.Id.ToString());


        logger.LogInformation("Updating restaurant with id {@RestaurantId}", request.Id);

        if (!restaurantauthorizationservice.Authorize(restaurantFromDb, ResourceOperation.Update))
            throw new ForbidException();

        restaurantFromDb.Name = request.Name;

        restaurantFromDb.Description = request.Description;
        restaurantFromDb.HasDelivery = request.HasDelivery;

        await restaurantsRepository.UpdateRestaurantAsync(restaurantFromDb);
       
    }
}
