using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator :AbstractValidator<UpdateRestaurantCommand>
{

    private readonly List<string> validcategories = ["Italian", "Indian"];
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(d => d.Name)
            .Length(2, 100);


        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Enter restaurant description");

      
    }
}
