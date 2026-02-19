using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.RemoveUserRole;

public class RemoveUserRoleCommandHandler(ILogger<RemoveUserRoleCommandHandler> logger, UserManager<Domain.Entities.User> usermanager) : IRequestHandler<RemoveUserRoleCommand>
{
    public async Task Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing role {RoleName} from user with email {UserEmail}", request.RoleName, request.UserEmail);

        var user = await usermanager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);

        var hasrole = await usermanager.IsInRoleAsync(user, request.RoleName);

        if (hasrole)
            await usermanager.RemoveFromRoleAsync(user, request.RoleName);
    }
}
