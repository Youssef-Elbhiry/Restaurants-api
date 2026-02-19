using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(IMapper mapper , IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantsDto>
{
    public async Task<RestaurantsDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
        var restaurantsDto = mapper.Map<RestaurantsDto>(restaurant);

        return restaurantsDto;
    }
}
