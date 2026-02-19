using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator :AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validcategories = ["Italian", "Indian"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(d => d.Name)
            .Length(2, 100);

        RuleFor(d=>d.ContactEmail)
            .EmailAddress()
            .WithMessage("Invalid email address format");


        RuleFor(d=>d.PostalCode)
            .Matches(@"^\d{2}-\d{3}")
            .WithMessage("Postal code must be in the format XX-XXX");


        RuleFor(d => d.Category)
            .Must(v => validcategories.Contains(v));
    }
}
