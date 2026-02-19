using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Xunit;

namespace Restaurants.API.Middlewares.Tests;

public class ErrorHandlingMiddlewareTests
{
    [Fact()]
    public async Task InvokeAsync_WhenNoExceptionThrown_ShoudInvokeNext()
    {
        //arrange
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

        var Middleware = new ErrorHandlingMiddleware(loggerMock.Object);

        var httpContext = new DefaultHttpContext();

        var next = new Mock<RequestDelegate>();
        next.Setup(n=>n.Invoke(httpContext)).Returns(Task.CompletedTask);



        //act
       await Middleware.InvokeAsync(httpContext, next.Object);

        //assert

        next.Verify(n=>n.Invoke(httpContext), Times.Once);
    }

    [Fact()]
    public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShoudSetStatusCode404()
    {
        //arrange
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

        var Middleware = new ErrorHandlingMiddleware(loggerMock.Object);

        var httpContext = new DefaultHttpContext();

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(httpContext)).Throws(new NotFoundException(nameof(Restaurant),"1"));



        //act
        await Middleware.InvokeAsync(httpContext, next.Object);

        //assert

       httpContext.Response.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact()]
    public async Task InvokeAsync_WhenForbidExceptionThrown_ShoudSetStatusCode403()
    {
        //arrange
        var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();

        var Middleware = new ErrorHandlingMiddleware(loggerMock.Object);

        var httpContext = new DefaultHttpContext();

        var next = new Mock<RequestDelegate>();
        next.Setup(n => n.Invoke(httpContext)).Throws(new ForbidException());



        //act
        await Middleware.InvokeAsync(httpContext, next.Object);

        //assert

        httpContext.Response.StatusCode.Should().Be(StatusCodes.Status403Forbidden);
    }
}