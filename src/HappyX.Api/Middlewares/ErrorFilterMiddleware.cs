using Microsoft.EntityFrameworkCore;

namespace HappyX.Api.Middlewares;

public class ErrorFilterMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorFilterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/plain"; 
            await context.Response.WriteAsync("Un Expected Error");
        }
    }
}

