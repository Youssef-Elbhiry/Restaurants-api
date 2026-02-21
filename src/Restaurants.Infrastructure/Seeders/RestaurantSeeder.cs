using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Seeders;


internal class RestaurantSeeder(RestaurantDbContext dbContext) : IRestaurantSeeder
{
    public async Task SeedAsync()
    {
        if(dbContext.Database.GetPendingMigrations().Any())
        {
            await dbContext.Database.MigrateAsync();
        }

        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();

                dbContext.Restaurants.AddRange(restaurants);

                await dbContext.SaveChangesAsync();
            }
            if(!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }


    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
        [
            new IdentityRole(UserRole.Admin),
            new IdentityRole(UserRole.Owner),
            new IdentityRole(UserRole.User)

        ];
        return roles;
    }
    private List<Restaurant> GetRestaurants()
    {
        var owner = new User
        {
            Email = "test-seed@test.com"
        };

        List<Restaurant> restaurants =
        [
            new Restaurant
            {
              Owner = owner,
              Name = "KFC",
              Category = "Fast Food",
              Description ="KFC is an american fast food restaurant",
              ContactEmail = "contact@kfc.com",
              ContactNumber = "1234560",
              HasDelivery = true,
              Dishes = [
                  new Dish{
                      Name = "Nashville Hot Chicken ",
                      Description = "Nashville Hot Chicken (10 pics)",
                      Price =10.50M
                  }
                  ],
              Address = new Address{
                  City = "London",
                  Street = "cork st 5",
                  PostalCode = "WC2N 50U"
              }
            }
        ];

        return restaurants;
    }
}
