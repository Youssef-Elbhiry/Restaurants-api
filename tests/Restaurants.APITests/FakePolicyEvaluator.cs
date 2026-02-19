using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Restaurants.API.Constants;
using System.Security.Claims;

namespace Restaurants.APITests;

internal class FakePolicyEvaluator : IPolicyEvaluator
{
    public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
    {
        var claims = new[]
        {
          new Claim(ClaimTypes.NameIdentifier, "1"),
          new Claim(ClaimTypes.Role ,UserRole.Owner)
      };
        var identity = new ClaimsIdentity(claims, "Test");
       
        var user = new ClaimsPrincipal(identity);

        var ticket = new AuthenticationTicket(user, "Test");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }

    public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
    {
      var result = PolicyAuthorizationResult.Success();
        return Task.FromResult(result);
    }
}
