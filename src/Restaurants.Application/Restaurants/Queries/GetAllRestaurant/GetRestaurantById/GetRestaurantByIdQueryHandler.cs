using AutoMapper;
using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(IMapper mapper , IRestaurantsRepository restaurantsRepository, IBlobStorageService blobStorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantsDto>
{
    public async Task<RestaurantsDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        var restaurantsDto = mapper.Map<RestaurantsDto>(restaurant);

        restaurantsDto.LogoUrl = blobStorageService.GetBlobSasUrl(restaurant.LogoUri);

        return restaurantsDto;
    }
}
