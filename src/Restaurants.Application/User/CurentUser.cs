namespace Restaurants.Application.User;

public record CurentUser(string UserId , string Email, IEnumerable<string> Roles, DateOnly? DateOfBirth , string? Nationality)
{
  
    public bool IsInRole(string role) => Roles.Contains(role);
    
   
}
