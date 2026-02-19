
using MediatR;

namespace Restaurants.Application.User.Commands.UpdateUser;

public class UpdateUserCommand : IRequest
{
    public string Nationality { get; set; }
    public DateOnly DateOfBirth { get; set; }
}
