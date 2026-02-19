using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
        {
            return await dbContext.Restaurants.Include(r => r.Dishes).ToListAsync();
        }

        public async Task<(IEnumerable<Restaurant> , int)> GetAllMatchingAsync(string? SearchPhrase, int PageNumber, int PageSize , string? SortBy , SortDirection? sortDirection)
        {
            var SearchPhraseLower = SearchPhrase?.ToLower();

            var query = dbContext.Restaurants.Include(r => r.Dishes)
                .Where(r => string.IsNullOrEmpty(SearchPhraseLower) || r.Name.ToLower().Contains(SearchPhraseLower));

            var totalItems = await query.CountAsync();

            if(!String.IsNullOrEmpty(SortBy))
            {
                var columsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                      {
                        nameof(Restaurant.Name) , r => r.Name
                      },
                      {
                        nameof(Restaurant.Category) , r => r.Category
                      },
                       {
                        nameof(Restaurant.Address.City) , r => r.Address!.City
                      }
                    };

                    query = sortDirection ==SortDirection.Ascending ? query.OrderBy(columsSelector[SortBy]) :  query.OrderByDescending(columsSelector[SortBy]);

            }

            var restaurants =  await query.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToListAsync();

            

            return (restaurants , totalItems);
        }

        public async Task<Restaurant> GetRestaurantByIdAsync(int id)
        {
            return await dbContext.Restaurants.Include(r => r.Dishes).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<int> AddRestaurantAsync(Restaurant restaurant)
        {
            await dbContext.Restaurants.AddAsync(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task DeleteRestaurantAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Remove(restaurant);
            await dbContext.SaveChangesAsync();
        }

        public async Task UpdateRestaurantAsync(Restaurant restaurant)
        {
            dbContext.Restaurants.Update(restaurant);
            await dbContext.SaveChangesAsync();
        }

       
    }
}
