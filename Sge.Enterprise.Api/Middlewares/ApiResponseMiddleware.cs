using System.Text;
using System.Text.Json;
using Sge.Enterprise.Api.Utilities;

namespace Sge.Enterprise.Api.Middlewares
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // No envolver swagger
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                await _next(context);
                return;
            }

            var originalBodyStream = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            // 🔹 No hay try/catch aquí: las excepciones las maneja ExceptionHandlingMiddleware
            await _next(context);

            // Volver al inicio
            memoryStream.Seek(0, SeekOrigin.Begin);
            var bodyAsText = await new StreamReader(memoryStream).ReadToEndAsync();

            // Si no hay contenido
            if (string.IsNullOrWhiteSpace(bodyAsText))
            {
                var emptyResponse = new ApiResponse(
                    data: null,
                    status: context.Response.StatusCode is >= 200 and < 300,
                    message: GetDefaultMessage(context.Response.StatusCode));

                var wrapperJson = JsonSerializer.Serialize(emptyResponse, _jsonOptions);

                context.Response.ContentType = "application/json; charset=utf-8";
                context.Response.ContentLength = Encoding.UTF8.GetByteCount(wrapperJson);

                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(wrapperJson);
                return;
            }

            // ⚠️ Solo envolvemos si es 2xx
            if (context.Response.StatusCode is < 200 or >= 300)
            {
                // No tocamos errores (los manejó ExceptionHandlingMiddleware)
                context.Response.Body = originalBodyStream;
                await context.Response.WriteAsync(bodyAsText);
                return;
            }

            // Intentar deserializar body original
            object? originalData;
            try
            {
                originalData = JsonSerializer.Deserialize<object>(bodyAsText,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                originalData = bodyAsText;
            }

            var apiResponse = new ApiResponse(
                data: originalData,
                status: true,
                message: GetDefaultMessage(context.Response.StatusCode));

            var json = JsonSerializer.Serialize(apiResponse, _jsonOptions);

            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(json);

            context.Response.Body = originalBodyStream;
            await context.Response.WriteAsync(json);
        }

        private static string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status200OK => "Proceso exitoso",
                StatusCodes.Status201Created => "Recurso creado exitosamente",
                StatusCodes.Status202Accepted => "Solicitud aceptada para procesamiento",
                StatusCodes.Status204NoContent => "Sin contenido",
                _ => "Proceso finalizado"
            };
        }
    }
}