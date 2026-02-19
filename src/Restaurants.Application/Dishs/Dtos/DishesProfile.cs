using AutoMapper;
using Restaurants.Application.Dishs.Commands.CreateDish;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishs.Dtos;

public class DishesProfile : Profile
{

    public DishesProfile()
    {
        CreateMap<Dish, DishDto>();
        CreateMap<CreateDishCommand , Dish>();

        CreateMap<DishDto,Dish>();
    }
}
