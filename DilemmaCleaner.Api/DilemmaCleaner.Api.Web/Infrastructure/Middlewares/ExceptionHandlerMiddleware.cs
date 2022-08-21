using System.Net;
using DilemmaCleaner.Api.Web.Infrastructure.Exceptions;
using Newtonsoft.Json;

namespace DilemmaCleaner.Api.Web.Infrastructure.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
                BadRequestException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            _logger.LogError(error.Message);
            
            var result = JsonConvert.SerializeObject(new ExceptionModel(error.Message));
            await response.WriteAsync(result);
        }
    }
}

public record ExceptionModel(string Message);
