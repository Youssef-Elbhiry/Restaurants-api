using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Repositories;

namespace Restaurants.Infrastructure.Authorization;

public class MinimumRestaurantRequirementHandler(IUserContext usercontext , IRestaurantsRepository restaurantsRepository , ILogger<MinimumRestaurantRequirementHandler> logger) : AuthorizationHandler<MinimumRestaurantRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantRequirement requirement)
    {
       
        var user = usercontext.GetCurentUser();

        logger.LogInformation("Handling Minimum restaurant requirment for user {@user}", user);

        var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();

        if (restaurants.Where(r => r.OwnerId == user.UserId).Count() >= requirement.MinimumRestaurants)
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("User does not meet the minimum restaurant requirement");
            context.Fail();
        }

       
    }
}
