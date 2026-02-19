using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extensions;
using Serilog;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.API.Extensions;
try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    builder.AddPresentation();

    var app = builder.Build();

    var scope = app.Services.CreateScope();

    var restaurantSeeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();

    await restaurantSeeder.SeedAsync();



    // Configure the HTTP request pipeline.
    app.UseMiddleware<TimeLoggingMiddleware>();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
   


    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();
    app.MapGroup("api/Identity")
       .WithTags("Identity")
       .MapIdentityApi<User>();
    app.Run();


}
 catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}
public partial class Program { }    