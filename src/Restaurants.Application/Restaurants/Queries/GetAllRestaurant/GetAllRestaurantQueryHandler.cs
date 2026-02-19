
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Comons;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQueryHandler (IRestaurantsRepository restaurantsRepository , IMapper mapper , ILogger<GetAllRestaurantQueryHandler> logger): IRequestHandler<GetAllRestaurantQuery, PagedResult<RestaurantsDto>>
{
    public async Task<PagedResult<RestaurantsDto>> Handle(GetAllRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all restaurants");

        var resulttuble = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase , request.PageNumber 
            , request.PageSize , request.SortBy , request.SortDirection);

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantsDto>>(resulttuble.Item1);


        var pageresult = new PagedResult<RestaurantsDto>(restaurantsDtos, resulttuble.Item2, request.PageSize, request.PageNumber);
   
        return pageresult;
    }
}
