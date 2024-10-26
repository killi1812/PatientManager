using System.Net;
using Microsoft.AspNetCore.Http;

namespace PatientManagerServices.Extras;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {

        ExceptionResponse response;
        switch (exception)
        {
            case NotFoundException:
                response = new ExceptionResponse(HttpStatusCode.NotFound, exception.Message);
                break;
            case UnauthorizedException:
                response = new ExceptionResponse(HttpStatusCode.Unauthorized, exception.Message);
                break;
            default:
                response = new ExceptionResponse(HttpStatusCode.InternalServerError, exception.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.Status;
        await context.Response.WriteAsync(response.ToJson());
    }
}