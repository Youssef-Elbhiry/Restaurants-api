using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.User.Commands.UpdateUser;

public class UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger , IUserContext userContext,IUserStore<Domain.Entities.User> userstore ) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurentUser();
        logger.LogInformation("Updateing user:{id} with {@request}", user?.UserId, request);

       var dbuser = await userstore.FindByIdAsync(user?.UserId, cancellationToken);

        if (dbuser == null) throw new NotFoundException(nameof(Domain.Entities.User), user.UserId);

         dbuser.DateOfBirth = request.DateOfBirth;
         dbuser.Nationality = request.Nationality;

       await userstore.UpdateAsync(dbuser, cancellationToken);
    }
}
