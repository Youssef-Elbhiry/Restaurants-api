using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurants.Application.User;
using Restaurants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurants.ApplicationTests.User;

public class UserContextTests
{
    [Fact()]
    public void GetCurentUser_WithAuthenticatedUser_ShouldReturnCurentUser()
    {
        // arrange
        var HttpContextAccessorMock = new Mock<IHttpContextAccessor>();


        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Email, "test@gmail.com"),
            new Claim(ClaimTypes.Role ,UserRole.Admin ),
            new Claim(ClaimTypes.Role ,UserRole.User ),
            new Claim("Nationality","Indian"),
            new Claim("DateOfBirth","1999-01-01")
        };

        var identity = new ClaimsIdentity(claims, "TestAuthType");

        var user = new ClaimsPrincipal(identity);

        HttpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext
        {
            User = user,
        });


        var userContext = new UserContext(HttpContextAccessorMock.Object);

        // act

        var curentUser = userContext.GetCurentUser();

        // assert

        curentUser.Should().NotBeNull();

        curentUser.UserId.Should().Be("1");
        curentUser.Email.Should().Be("test@gmail.com");
        curentUser.Roles.Should().Contain(UserRole.Admin);
        curentUser.Nationality.Should().Be("Indian");
        curentUser.DateOfBirth.Should().Be(DateOnly.Parse("1999-01-01"));

    }
    [Fact()]
    public void GetCurentUser_WithNoAuthenticatedUser_ShouldReturnCurentUser()
    {
        // arrange
        var HttpContextAccessorMock = new Mock<IHttpContextAccessor>();



        HttpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);


        var userContext = new UserContext(HttpContextAccessorMock.Object);

        // act

        var action =() => userContext.GetCurentUser();

        // assert

        action.Should().Throw<Exception>().WithMessage("Invalid user context");

    }
}