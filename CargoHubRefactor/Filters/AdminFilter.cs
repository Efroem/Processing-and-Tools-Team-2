using Microsoft.AspNetCore.Mvc.Filters;

public class AdminFilter : IAsyncActionFilter
{
    private readonly IConfiguration _configuration;

    public AdminFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
    {
        var context = actionContext.HttpContext;

        var expectedToken = _configuration["ApiKeys:ApiToken"]; 

        if (string.IsNullOrEmpty(expectedToken))
        {
            Console.WriteLine("Check om te kijken of hij inderdaad in appsettings staat (error)");
            context.Response.StatusCode = 500; 
            await context.Response.WriteAsync("API token is niet goed geconfigured");
            return;
        }

        if (!context.Request.Headers.ContainsKey("ApiToken"))
        {
            Console.WriteLine($"{context.Request.Path} was requested but there is no ApiToken header");
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Missing ApiToken header.");
            return;
        }

        if (context.Request.Headers["ApiToken"] != expectedToken)
        {
            Console.WriteLine($"{context.Request.Path} was requested but the ApiToken is {context.Request.Headers["ApiToken"]} instead of the expected value");
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid ApiToken.");
            return;
        }

        await next();
    }
}
