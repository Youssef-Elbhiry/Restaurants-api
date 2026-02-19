using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middlewares;
using Restaurants.Domain.Entities;
using Serilog;

namespace Restaurants.API.Extensions;

public static class WepApplicationBuilderExtension
{

    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication();
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
            .ReadFrom.Configuration(context.Configuration);
            //.MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
            //.MinimumLevel.Override("Microsoft.EntityFrameWorkCore",LogEventLevel.Information)
            //.WriteTo.Console(outputTemplate: "[{Timestamp: dd:MM HH:mm:ss} {Level:u3}] |{SourceContext}|{NewLine}{Message:lj}{NewLine}{Exception}")
            //.WriteTo.File("Logs/Restaurant.Api-.log",rollingInterval:RollingInterval.Day,rollOnFileSizeLimit:true);
        });
       
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<TimeLoggingMiddleware>();

        builder.Services.AddEndpointsApiExplorer();
        // configure swagger to use the Bearer token authentication
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                 Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                 }
            },
            []
        }
    });
        });

        builder.Services.AddScoped(typeof(ErrorHandlingMiddleware));

    }
}
