using MediatR;

namespace Restaurants.Application.User.Commands.RemoveUserRole;

public class RemoveUserRoleCommand : IRequest
{

    public string UserEmail { get; set; }
    public string RoleName { get; set; }
}
