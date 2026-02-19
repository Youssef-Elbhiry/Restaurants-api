using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.ApplicationTests.Restaurants.Dtos;

public class RestaurantsProfileTests
{
    private IMapper _mapper;

    public RestaurantsProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantsProfile>();
        });

        _mapper = configuration.CreateMapper();
    }


    [Fact()]
    public void RestaurantsProfile_MapFromCreateRestaurantCommandToRestaurant_ShoudMapCorectly()
    {
        // Arrange

        var createrestaurantCommand = new CreateRestaurantCommand
        {
            Name = "Test Restaurant",
            Description = "Test Description",
            Category = "Test Category",
            HasDelivery = true,
            ContactEmail = "test@KFC.com",
            ContactNumber = "123456789",
        };

        // act

        var restaurant = _mapper.Map<Restaurant>(createrestaurantCommand);

        //assert

        restaurant.Should().NotBeNull();
        restaurant.Name.Should().Be(createrestaurantCommand.Name);
        restaurant.Description.Should().Be(createrestaurantCommand.Description);
        restaurant.Category.Should().Be(createrestaurantCommand.Category);
        restaurant.HasDelivery.Should().Be(createrestaurantCommand.HasDelivery);
        restaurant.ContactEmail.Should().Be(createrestaurantCommand.ContactEmail);
        restaurant.ContactNumber.Should().Be(createrestaurantCommand.ContactNumber);


    }

    [Fact()]

    public void RestaurantsProfile_MapFromRestaurantToRestaurantDto_ShoudMapCorectly()
    {
        // arrange
        var restaurant = new Restaurant
        {
            Id =1,
            Name = "Test",
            Description = "Test DESC",
            HasDelivery = true,
            ContactEmail = "Contact@Toma.com",
            Address = new Address
            {
                City = "Mansoura",
                Street = "10 el gala",
                PostalCode = "11-111"
            },
            ContactNumber = "11111"

        };

        //act 
       var restaurantdto = _mapper.Map<RestaurantsDto>(restaurant);

        //assert
        restaurantdto.Should().NotBeNull();
        restaurantdto.Name.Should().Be(restaurant.Name);
        restaurantdto.Id.Should().Be(restaurant.Id);
        restaurantdto.Description.Should().Be(restaurant.Description);
        restaurantdto.Category.Should().Be(restaurant.Category);
        restaurantdto.City.Should().Be(restaurant.Address.City);
        restaurantdto.Street.Should().Be(restaurant.Address.Street);
        restaurantdto.PostalCode.Should().Be(restaurant.Address.PostalCode);


    }
}