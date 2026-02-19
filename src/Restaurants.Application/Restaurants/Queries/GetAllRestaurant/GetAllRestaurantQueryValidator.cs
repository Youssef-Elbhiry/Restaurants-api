using FluentValidation;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurant;

public class GetAllRestaurantQueryValidator : AbstractValidator<GetAllRestaurantQuery>
{
    private int[] allowedsizes = [5, 10, 15, 30];

    private string[] allowedsortby = [nameof(Restaurant.Name), nameof(Restaurant.Category), nameof(Restaurant.Address.City)];
    public GetAllRestaurantQueryValidator()
    {
        RuleFor(r=>r.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1");



        RuleFor(r=>r.PageSize)
            .Must(value=> allowedsizes.Contains(value))
            .WithMessage($"Page size must be one of the following values: {string.Join(',' ,allowedsizes)}");


            RuleFor(r=>r.SortBy)         
            .Must(value =>allowedsortby.Contains(value))
            .When(r => r.SortBy != null)
            .WithMessage($"Sort by  is optional , or must be one of the following values: {string.Join(',' ,allowedsortby)}");
    }
}
