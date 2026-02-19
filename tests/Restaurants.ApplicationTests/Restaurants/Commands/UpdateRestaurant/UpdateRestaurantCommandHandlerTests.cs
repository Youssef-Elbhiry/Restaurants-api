using Xunit;
using Moq;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Constants;
using Restaurants.Application.User;
using AutoMapper;
using System.Threading.Tasks;
using FluentAssertions;
using Restaurants.Domain.Exceptions;

namespace Restaurants.ApplicationTests.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForAuthUserWhoCanUpdate_ShouldUpdateSuccessfuly()
    {
        //arrange
        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            HasDelivery = true,
            OwnerId = "user1"
        };
        restaurantsRepositoryMock.Setup(r => r.GetRestaurantByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(restaurant);

        restaurantsRepositoryMock.Setup(r => r.UpdateRestaurantAsync(It.IsAny<Restaurant>()))
            .Returns(Task.CompletedTask);


        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        restaurantAuthorizationServiceMock.Setup(r => r.Authorize(It.IsAny<Restaurant>(), It.IsAny<ResourceOperation>()))
            .Returns(true);

        var usercontextMock = new Mock<IUserContext>();
        var currentUser = new CurentUser("user1", "owner@gmail.com", [UserRole.Owner], null, null);
        usercontextMock.Setup(u=> u.GetCurentUser()).Returns(currentUser);

        var handler = new UpdateRestaurantCommandHandler(restaurantsRepositoryMock.Object, loggerMock.Object, restaurantAuthorizationServiceMock.Object);

        var command = new UpdateRestaurantCommand
        {         
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = false
        };
        //act 
        await handler.Handle(command, CancellationToken.None);

        //assert

        restaurant.Name.Should().Be(command.Name);
        restaurant.Description.Should().Be(command.Description);
        restaurant.HasDelivery.Should().Be(command.HasDelivery);

        restaurantsRepositoryMock.Verify(r => r.GetRestaurantByIdAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact()]
    public  void Handle_ForAuthUserWhoCannotUpdate_ShouldThrowForbidException()
    {
        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

        var restaurant = new Restaurant
        {
            Id = 1,
            Name = "Test Restaurant",
            Description = "Test Description",
            HasDelivery = true,
            OwnerId = "user1"
        };
        restaurantsRepositoryMock.Setup(r => r.GetRestaurantByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(restaurant);

        restaurantsRepositoryMock.Setup(r => r.UpdateRestaurantAsync(It.IsAny<Restaurant>()))
            .Returns(Task.CompletedTask);


        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        restaurantAuthorizationServiceMock.Setup(r => r.Authorize(It.IsAny<Restaurant>(), It.IsAny<ResourceOperation>()))
            .Returns(false);

        var usercontextMock = new Mock<IUserContext>();
        var currentUser = new CurentUser("user1", "owner@gmail.com", [UserRole.Owner], null, null);
        usercontextMock.Setup(u => u.GetCurentUser()).Returns(currentUser);

        var handler = new UpdateRestaurantCommandHandler(restaurantsRepositoryMock.Object, loggerMock.Object, restaurantAuthorizationServiceMock.Object);

        var command = new UpdateRestaurantCommand
        {
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = false
        };

        //act

      var action = async()=>await handler.Handle(command, CancellationToken.None);


        //assert
        action.Should().ThrowAsync<ForbidException>();
     
    }

    [Fact()]
    public void Handle_ForAuthUserWhenReturnNullRestaurant_ShouldNotFoundException()
    {
        var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();

       
        restaurantsRepositoryMock.Setup(r => r.GetRestaurantByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Restaurant)null);

        restaurantsRepositoryMock.Setup(r => r.UpdateRestaurantAsync(It.IsAny<Restaurant>()))
            .Returns(Task.CompletedTask);


        var loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();

        var restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

        restaurantAuthorizationServiceMock.Setup(r => r.Authorize(It.IsAny<Restaurant>(), It.IsAny<ResourceOperation>()))
            .Returns(false);

        var usercontextMock = new Mock<IUserContext>();
        var currentUser = new CurentUser("user1", "owner@gmail.com", [UserRole.Owner], null, null);
        usercontextMock.Setup(u => u.GetCurentUser()).Returns(currentUser);

        var handler = new UpdateRestaurantCommandHandler(restaurantsRepositoryMock.Object, loggerMock.Object, restaurantAuthorizationServiceMock.Object);

        var command = new UpdateRestaurantCommand
        {
            Name = "Updated Restaurant",
            Description = "Updated Description",
            HasDelivery = false
        };

        //act

        var action = async () => await handler.Handle(command, CancellationToken.None);


        //assert
        action.Should().ThrowAsync<NotFoundException>();

    }
}