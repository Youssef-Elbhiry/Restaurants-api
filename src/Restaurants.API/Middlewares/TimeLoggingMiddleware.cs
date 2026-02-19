
namespace Restaurants.API.Middlewares;

public class TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var starttime = DateTime.Now;
        await  next.Invoke(context);
        var timetaken = DateTime.Now - starttime;
        logger.LogInformation($"Request :{context.Request.Path} ,Verb : {context.Request.Method} , TimeTakenByRequest :{timetaken}");

    }
}
