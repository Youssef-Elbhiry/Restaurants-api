using MediatR;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetAllDishesForRestaurant;

public class GetAllDishesForRestaurantQuery :IRequest<List<DishDto>>
{
    public int RestaurantId { get;  }
    public GetAllDishesForRestaurantQuery(int id)
    {
        RestaurantId = id;
    }
}
