using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant.GetRestaurantById;

public class GetRestaurantByIdQuery : IRequest<RestaurantsDto>
{
    public GetRestaurantByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get;  }
}
