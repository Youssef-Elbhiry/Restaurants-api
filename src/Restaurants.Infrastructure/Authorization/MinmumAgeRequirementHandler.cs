using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;

namespace Restaurants.Infrastructure.Authorization;

internal class MinmumAgeRequirementHandler(ILogger<MinmumAgeRequirementHandler> logger , 
    IUserContext userContext) : AuthorizationHandler<MinmumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinmumAgeRequirement requirement)
    {
        var user = userContext.GetCurentUser();
        logger.LogInformation("Handling Minmum age requirment for user :{user}", user);

        if(user == null || user.DateOfBirth == null)
        {
            logger.LogWarning("User is not authenticated or date of birth claim is missing");
            context.Fail();
            return Task.CompletedTask;
        }

        if (user.DateOfBirth.Value.AddYears(requirement.MinmumAge) <= DateOnly.FromDateTime(DateTime.Now))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogWarning("User does not meet the minimum age requirement");
            context.Fail();
        }
        return Task.CompletedTask;

    }
}
