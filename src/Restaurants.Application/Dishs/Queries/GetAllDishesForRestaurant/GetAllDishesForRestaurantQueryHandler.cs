

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishs.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQueryHandler(IDishRepository dishRepository , IMapper mapper,ILogger<GetAllDishesForRestaurantQueryHandler> logger , IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllDishesForRestaurantQuery, List<DishDto>>
{
    public async Task<List<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving all dishes for restaurant with ID {RestaurantId}", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if(restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        var dishes = await dishRepository.GetAll(request.RestaurantId);



        var dishDtos = mapper.Map<List<DishDto>>(dishes);
        return dishDtos;


    }
}
