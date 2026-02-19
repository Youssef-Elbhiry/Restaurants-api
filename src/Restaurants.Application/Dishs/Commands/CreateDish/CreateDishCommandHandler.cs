using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishs.Commands.CreateDish;

public class CreateDishCommandHandler(IDishRepository dishRepository , IMapper mapper ,ILogger<CreateDishCommandHandler> logger , IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new dish for restaurant with ID {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null) throw new NotFoundException(nameof(Restaurant) , request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        var dishId = await dishRepository.CreateDish(dish);

        return dishId;
    }
}
