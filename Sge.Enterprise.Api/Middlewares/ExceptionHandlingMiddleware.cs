using System.Text.Json;
using Sge.Enterprise.Api.Utilities;
using Sge.Enterprise.Application.Exceptions;

namespace Sge.Enterprise.Api.Middlewares
{
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
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json; charset=utf-8";

            var statusCode = exception switch
            {
                NotFoundException   => StatusCodes.Status404NotFound,
                BadRequestException => StatusCodes.Status400BadRequest,
                _                   => StatusCodes.Status500InternalServerError
            };

            httpContext.Response.StatusCode = statusCode;

            var response = new ApiResponse(
                data: null,
                status: false,
                message: exception.Message // 👈 aquí va tu "No encontró empleado"
            );

            var json = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(json);
        }
    }
}