using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishs.Queries.GetDishById;

public class GetDishByIdQueryHandler (IDishRepository dishrepository,IMapper mapper,ILogger<GetDishByIdQueryHandler> logger , IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetDishByIdQuery, DishDto>
{
    public async Task<DishDto> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish with ID {DishId} for restaurant with ID {RestaurantId}", request.DishId, request.RestaurantId);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
       
        var dish = await dishrepository.GetById(request.RestaurantId, request.DishId);

        if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());


         var dishDto = mapper.Map<DishDto>(dish);
        return dishDto;
    }
}
