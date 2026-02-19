using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler (ILogger<DeleteRestaurantCommandHandler> logger , IRestaurantsRepository restaurantsRepository,
    IRestaurantAuthorizationService restaurantauthorizationservice) : IRequestHandler<DeleteRestaurantCommand>
{
    public  async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant),request.Id.ToString());

        if(!restaurantauthorizationservice.Authorize(restaurant , ResourceOperation.Delete))
         throw new ForbidException();

        logger.LogInformation("Deleted restaurant with id {@RestaurantId}", request.Id);
        await  restaurantsRepository.DeleteRestaurantAsync(restaurant);
   
    }
}
