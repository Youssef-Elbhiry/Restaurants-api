using AutoMapper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{

    public RestaurantsProfile()
    {
        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(dist => dist.Address, opt => opt.MapFrom(src => new Address
            {
                City = src.City,
                Street = src.Street,
                PostalCode = src.PostalCode
            }));
          



        CreateMap<Restaurant, RestaurantsDto>()
          .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
          .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
          .ForMember(dist => dist.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
          .ForMember(dist => dist.Dishes, opt => opt.MapFrom(src=> src.Dishes));

    }
}
