using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishs.Commands.DeleteDishFromRestaurant;

public class DeleteDishFromRestaurantCommandHandler(ILogger<DeleteDishFromRestaurantCommandHandler> logger ,
    IRestaurantsRepository restaurantsRepository
    , IDishRepository dishRepository ,IRestaurantAuthorizationService restaurantauthorizationservice) : IRequestHandler<DeleteDishFromRestaurantCommand>
{
    public async Task Handle(DeleteDishFromRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting dish with id {DishId} from restaurant with id {RestaurantId}", request.DishId, request.RestaurantId);
        
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);

        if (restaurant == null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

        var dish = await dishRepository.GetById(request.RestaurantId, request.DishId);

        if (dish == null) throw new NotFoundException(nameof(Dish),request.DishId.ToString());

        if (!restaurantauthorizationservice.Authorize(restaurant, ResourceOperation.Delete))
            throw new ForbidException();

        await dishRepository.DeleteDish(dish);
    }
}
