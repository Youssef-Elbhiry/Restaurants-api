using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IDishRepository
{
    Task<List<Dish>> GetAll(int RestaurantId);
    Task<int> CreateDish(Dish dish);
    Task<Dish> GetById(int Restaurantid, int DishId);
    Task DeleteDish(Dish dish);
}
