using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization;

public class MinimumRestaurantRequirement(int minimumRestaurants) : IAuthorizationRequirement
{
    public int MinimumRestaurants { get;  } = minimumRestaurants;
}
