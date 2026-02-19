
using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization;

public class MinmumAgeRequirement(int minmumage) : IAuthorizationRequirement
{
        public int MinmumAge { get; } = minmumage;
}
