using MediatR;

namespace Restaurants.Application.Dishs.Commands.DeleteDishFromRestaurant;

public class DeleteDishFromRestaurantCommand(int rid , int did) : IRequest
{
    public int RestaurantId { get;  } = rid;
    public int DishId { get; } = did;
}
