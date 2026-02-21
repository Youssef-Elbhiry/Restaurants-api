using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.API.Constants;
using Restaurants.Application.User.Commands.AssignUserRole;
using Restaurants.Application.User.Commands.RemoveUserRole;
using Restaurants.Application.User.Commands.UpdateUser;

namespace Restaurants.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IdentityController(IMediator mediator) : ControllerBase
{
    [HttpPatch("User")]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    [HttpPost("UserRole")]
   // [Authorize(Roles = UserRole.Admin)]

    public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
    {
         await mediator.Send(command);

        return NoContent();
    }


    [HttpDelete("UserRole")]
    [Authorize(Roles = UserRole.Admin)]

    public async Task<IActionResult> RemoveUserRole(RemoveUserRoleCommand command)
    {
         await mediator.Send(command);
        return NoContent();
    }


}
