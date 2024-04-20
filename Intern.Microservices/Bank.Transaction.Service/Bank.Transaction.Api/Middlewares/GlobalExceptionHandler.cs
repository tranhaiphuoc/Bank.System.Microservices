using Bank.Transaction.Api.Responses;
using Bank.Transaction.Domain.Errors;
using System.Net.Mime;

namespace Bank.Transaction.Api.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

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

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var error = exception switch
        {
            _ => ErrorMessage.Exception
        };

        context.Response.ContentType = MediaTypeNames.Application.Json;
        context.Response.StatusCode = error.StatusCode;

        await context.Response.WriteAsJsonAsync(new ResponseObject(error.Code, error.Message));
    }
}
