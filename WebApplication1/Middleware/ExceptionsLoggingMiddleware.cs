using System.Diagnostics;

namespace WebApplication1.Middleware;

public class ExceptionsLoggingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex, "An unhandled exception occurred");
            throw;
        }
    }
}