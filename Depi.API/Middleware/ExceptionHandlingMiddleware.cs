using System.Net;
using System.Text.Json;

namespace DEPI.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorResponse) = exception switch
        {
            ArgumentException argEx => (
                HttpStatusCode.BadRequest,
                new ErrorResponse("validation_error", argEx.Message)),

            UnauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                new ErrorResponse("unauthorized", "You are not authorized to perform this action")),

            InvalidOperationException invEx => (
                HttpStatusCode.BadRequest,
                new ErrorResponse("business_error", invEx.Message)),

            KeyNotFoundException => (
                HttpStatusCode.NotFound,
                new ErrorResponse("not_found", "The requested resource was not found")),

            FluentValidation.ValidationException valEx => (
                HttpStatusCode.BadRequest,
                new ErrorResponse("validation_error", "One or more validation errors occurred",
                    valEx.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}").ToList())),

            _ => (
                HttpStatusCode.InternalServerError,
                new ErrorResponse("internal_error", exception.Message, new List<string> { exception.StackTrace ?? "no stack" }))
        };

        context.Response.StatusCode = (int)statusCode;

        var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}

public record ErrorResponse(string Code, string Message, List<string>? Details = null);