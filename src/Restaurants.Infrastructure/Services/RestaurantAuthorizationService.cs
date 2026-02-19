using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Services;

public class RestaurantAuthorizationService(IUserContext userContext , ILogger<RestaurantAuthorizationService> logger) : IRestaurantAuthorizationService
{

    public bool Authorize(Restaurant restaurant , ResourceOperation operation )
    {

        var user = userContext.GetCurentUser();

        logger.LogInformation("Authorizing user {useremail} for operation {operation} on restaurant {@Restaurant}", user.Email, operation, restaurant);

        if (operation == ResourceOperation.Read || operation == ResourceOperation.Create) { 
        
            logger.LogInformation("Read or Create operation - successful authorization");
            return true;
        }


        if(user.IsInRole(UserRole.Admin) && operation == ResourceOperation.Delete)
        {
            logger.LogInformation("Admin user - delete operation - successful authorization");
            return true;
        }

        if (operation == ResourceOperation.Update || operation == ResourceOperation.Delete && restaurant.OwnerId == user.UserId)
        {
            logger.LogInformation("Restaurant owner  - successful authorization");

            return true;

        }


        return false;
    }
}
