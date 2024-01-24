using System.ComponentModel.DataAnnotations;
using CarSharingApp.Identity.API.Middlewares.other;
using CarSharingApp.Identity.Shared.Exceptions;

namespace CarSharingApp.Identity.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var result = new ErrorDetails
        {
            StatusCode = 500,
            Title = exception.Message
        };

        switch (exception)
        {
            case ValidationException :
                result.StatusCode = 400;
                break;
            case NotFoundException  :
                result.StatusCode = 404;
                break;
            case BadAuthorizeException  :
                result.StatusCode = 400;
                break;
            case IdentityException  :
                result.StatusCode = 500;
                break;

        }

        context.Response.StatusCode = result.StatusCode;

        await context.Response.WriteAsync(result.ToString());
    }
}