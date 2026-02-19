using Restaurants.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization.Policy;
using Restaurants.APITests;
using Moq;
using Restaurants.Domain.Repositories;
using Restaurants.Domain.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.API.Controllers.Tests;

public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private Mock<IRestaurantsRepository> _mockRepo = new();

    public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
                {
                   // services.AddScoped<IPolicyEvaluator, FakePolicyEvaluator>();
                    services.Replace(ServiceDescriptor.Scoped<IRestaurantsRepository>(_ => _mockRepo.Object));
                });
        });
    }

    [Fact()]
    public async Task GetAll_ForValidRequest_ShoudReturn200Ok()
    {
        //arrange
        var client = _factory.CreateClient();

        //act
        var response = await client.GetAsync("/api/restaurants?PageNumber=1&PageSize=5");

        //assert

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact()]
    public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
    {
        //arrange
        var id = 2;

        _mockRepo.Setup(r => r.GetRestaurantByIdAsync(id)).ReturnsAsync((Restaurant?)null);


        var client = _factory.CreateClient();

        //act 

        var response = await client.GetAsync($"/api/restaurants/{id}");

        //assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    [Fact()]
    public async Task GetById_ForExistingId_ShouldReturn200Ok()
    {
        //arrange
        var id = 1000000;

        var restaurant = new Restaurant
        {
            Id = id,
            Name = "Test Restaurant",
            Description = "Test Description",


        };

        _mockRepo.Setup(r => r.GetRestaurantByIdAsync(id)).ReturnsAsync(restaurant);


        var client = _factory.CreateClient();

        //act 

        var response = await client.GetAsync($"/api/restaurants/{id}");

        var restaurantDto = await response.Content.ReadFromJsonAsync<RestaurantsDto>();
        //assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        restaurantDto.Should().NotBeNull();
        restaurantDto.Name.Should().Be(restaurant.Name);


    }
}