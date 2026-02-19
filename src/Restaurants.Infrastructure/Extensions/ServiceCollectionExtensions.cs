
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Persistence;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Services;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<RestaurantDbContext>(options =>
            options.UseSqlServer(
                config.GetConnectionString("RestaurantsDatabase")
                ).EnableSensitiveDataLogging()
        );

        services.AddIdentityApiEndpoints<User>()
           .AddRoles<IdentityRole>()
           .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
           .AddEntityFrameworkStores<RestaurantDbContext>();

        services.AddAuthorizationBuilder()
            .AddPolicy("Nationality", builder => builder.RequireClaim("Nationality" , "Polish", "British"))
            .AddPolicy("AtLeast20",builder => builder.AddRequirements( new MinmumAgeRequirement(20)))
            .AddPolicy("AtLeast2Restaurants", builder => builder.AddRequirements(new MinimumRestaurantRequirement(2)));

        services.AddScoped<IAuthorizationHandler, MinmumAgeRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, MinimumRestaurantRequirementHandler>();

        services.AddScoped< IRestaurantSeeder,RestaurantSeeder>();
        services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
        services.AddScoped<IDishRepository, DishRepository>();


        services.AddScoped<IRestaurantAuthorizationService , RestaurantAuthorizationService>();

        services.Configure<BlobStorageSettings>(config.GetSection("BlobStorage"));

        services.AddScoped<IBlobStorageService, BlobStorageService>();
    }
}
