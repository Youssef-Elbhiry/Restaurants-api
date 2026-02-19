using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishRepository(RestaurantDbContext dbContext) :IDishRepository
{

    public async Task<List<Dish>> GetAll(int RestaurantId)
    {
        var dishes = await dbContext.Dishs.Where(d => d.RestaurantId == RestaurantId).ToListAsync();
        return dishes;
    }
    public async Task<Dish> GetById(int Restaurantid , int DishId)
    {
        var dish = await dbContext.Dishs.FindAsync(DishId);
        if(dish is null || dish.RestaurantId != Restaurantid)
        {
            return null;
        }
        return dish;
    }
    public async Task<int> CreateDish(Dish dish)
    {
         dbContext.Dishs.Add(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task DeleteDish(Dish dish)
    {
        dbContext.Dishs.Remove(dish);
        await dbContext.SaveChangesAsync();
    }
}
