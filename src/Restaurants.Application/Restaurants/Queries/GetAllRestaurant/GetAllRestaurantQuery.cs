using MediatR;
using Restaurants.Application.Comons;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQuery :IRequest<PagedResult<RestaurantsDto>>
{
    public string? SearchPhrase { get; set; }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public string? SortBy { get; set; }

    public SortDirection? SortDirection { get; set; }

}
