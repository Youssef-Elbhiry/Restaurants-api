using MediatR;
using Restaurants.Application.Dishs.Dtos;

namespace Restaurants.Application.Dishs.Queries.GetDishById;

public class GetDishByIdQuery :IRequest<DishDto>
{
    public int RestaurantId { get; }
    public int DishId { get; }

    public GetDishByIdQuery( int restaurantId , int dishid)
    {
        RestaurantId = restaurantId;
        DishId = dishid;
    }
}
