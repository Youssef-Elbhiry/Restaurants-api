using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.User;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Tests;

public class MinmumAgeRequirementHandlerTests
{
    
    [Fact()]
    public async Task HandleRequirementAsync_UserHaveAgeGreterThan20_ShouldSuccessed()
    {
        //arange
        var userContextMock = new Mock<IUserContext>();

        var currentuser = new CurentUser("1", "test@gmail.com", [], new DateOnly(1990,1,1), null);
        userContextMock.Setup(u => u.GetCurentUser()).Returns(currentuser);

        var loggerMock = new Mock<ILogger<MinmumAgeRequirementHandler>>();

        var requirement = new MinmumAgeRequirement(20);

        var context = new AuthorizationHandlerContext([requirement] , null ,null);

        var handler = new MinmumAgeRequirementHandler(loggerMock.Object , userContextMock.Object);

        //act

        await handler.HandleAsync(context);

        //assert

        context.HasSucceeded.Should().BeTrue();
    }
    [Fact()]
    public async Task HandleRequirementAsync_UserHaveSmallerThan20_ShouldFail()
    {
        //arange
        var userContextMock = new Mock<IUserContext>();

        var currentuser = new CurentUser("1", "test@gmail.com", [], new DateOnly(2010, 1, 1), null);
        userContextMock.Setup(u => u.GetCurentUser()).Returns(currentuser);

        var loggerMock = new Mock<ILogger<MinmumAgeRequirementHandler>>();

        var requirement = new MinmumAgeRequirement(20);

        var context = new AuthorizationHandlerContext([requirement], null, null);

        var handler = new MinmumAgeRequirementHandler(loggerMock.Object, userContextMock.Object);

        //act

        await handler.HandleAsync(context);

        //assert

        context.HasSucceeded.Should().BeFalse();
    }
}