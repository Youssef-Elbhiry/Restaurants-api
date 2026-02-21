
using Restaurants.Application.Dishs.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public bool HasDelivery { get; set; }

    public string Category { get; set; }

     public  List<DishDto> Dishes { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string PostalCode { get; set; }
    public string? LogoUrl { get; internal set; }

    public static RestaurantsDto FromEntity(Restaurant r)
    {
        return new RestaurantsDto
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            Category = r.Category,
            HasDelivery = r.HasDelivery,
            City = r.Address.City,
            Street = r.Address.Street,
            PostalCode = r.Address.PostalCode,
            Dishes = r.Dishes.Select(DishDto.FromEntity).ToList()
        };

    }
}
