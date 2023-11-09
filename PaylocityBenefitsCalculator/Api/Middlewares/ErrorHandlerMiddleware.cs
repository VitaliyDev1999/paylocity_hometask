using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = error switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,// not found error
                _ => (int)HttpStatusCode.InternalServerError // unhandled error
            };

            _logger.LogError("{dateTypeUtc} Error: {error}", DateTime.UtcNow, error);
            var result = JsonSerializer.Serialize(new { message = error.Message });
            await response.WriteAsync(result);
        }
    }
}
