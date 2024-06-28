using HR.LeaveManagement.Api.Models;
using System.Net;
using HR.LeaveManagement.Application.Exceptions;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace HR.LeaveManagement.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problem = ex switch
            {
                BadRequestException badRequestException => new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)(statusCode = HttpStatusCode.BadRequest),
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                },
                NotFoundException NotFound => new CustomProblemDetails
                {
                    Title = NotFound.Message,
                    Status = (int)(statusCode = HttpStatusCode.NotFound),
                    Type = nameof(NotFoundException),
                    Detail = NotFound.InnerException?.Message,
                },
                _ => new CustomProblemDetails
                {
                    Title = ex.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = ex.StackTrace,
                }
            };
            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage= JsonSerializer.Serialize(problem);
            _logger.LogError(ex, logMessage);
            await httpContext.Response.WriteAsJsonAsync(problem);

        }
    }
}
