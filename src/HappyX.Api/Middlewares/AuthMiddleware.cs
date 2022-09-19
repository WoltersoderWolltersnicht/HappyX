using System.Text;

namespace HappyX.Api.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string password = context.Request.Headers["Authentication"].ToString();
        if (string.IsNullOrWhiteSpace(password) || password is not "ALX")
        {
            context.Response.StatusCode = 401;
            context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes($"Authentication {password} is invalid"));
        }
        await _next(context);
    }
}