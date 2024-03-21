using System.ComponentModel.DataAnnotations;
using CarSharingApp.Identity.API.Middlewares.other;
using CarSharingApp.Identity.Shared.Exceptions;

namespace CarSharingApp.Identity.API.Middlewares;

public class ExceptionAndLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionAndLoggingMiddleware> _logger;

    public ExceptionAndLoggingMiddleware(RequestDelegate next, ILogger<ExceptionAndLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        _logger.LogInformation($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");
        
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
        
        _logger.LogInformation($"Response: {httpContext.Response.StatusCode} {httpContext.Response}");
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
                result.StatusCode = 405;
                break;

        }
        
        context.Response.StatusCode = result.StatusCode;
        
        _logger.LogError($"Errors: {result.Title}");

        await context.Response.WriteAsync(result.Title);
    }
}