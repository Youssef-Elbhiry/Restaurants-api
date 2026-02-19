using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurentUser GetCurentUser()
    {
        var User = httpContextAccessor?.HttpContext?.User;

        if (User == null)
        {
            throw new Exception("Invalid user context");
        }

        if (User.Identity == null || !User.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value);

        var nationality = User.FindFirst(c => c.Type == "Nationality")?.Value;

        var dob = User.FindFirst(c => c.Type == "DateOfBirth")?.Value;

        var dateOfBirth = dob != null ? DateOnly.ParseExact(dob , "yyyy-mm-dd") : (DateOnly?)null;

        return new CurentUser(userId, email, roles, dateOfBirth, nationality);
    }
}
