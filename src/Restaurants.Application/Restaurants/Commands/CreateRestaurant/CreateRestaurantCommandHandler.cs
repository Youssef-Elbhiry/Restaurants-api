
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(IRestaurantsRepository restaurantsRepository , IMapper mapper 
    , ILogger<CreateRestaurantCommandHandler> logger , IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurentUser();

        var restaurant = mapper.Map<Restaurant>(request);

        restaurant.OwnerId = user.UserId;

        var id = await restaurantsRepository.AddRestaurantAsync(restaurant);

        logger.LogInformation(" user {useremail} Created a new restaurant with id {@Restaurant}",user.Email, restaurant);
        return id;
    }
}
