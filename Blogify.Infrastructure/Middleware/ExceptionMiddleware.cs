using Blogify.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Blogify.Infrastructure.Middleware
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
            var originalBodyStream = context.Response.Body;

            try
            {
                using var memoryStream = new MemoryStream();
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                if (!string.IsNullOrEmpty(responseBody) && responseBody.Contains("\"isSuccess\":"))
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    bool isSuccess = result.isSuccess;

                    if (!isSuccess)
                    {
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;

                        string errorMessage = result.error?.ToString() ?? string.Empty;

                        if (errorMessage.Contains("bulunamadı") || errorMessage.Contains("not found"))
                        {
                            context.Response.StatusCode = StatusCodes.Status404NotFound;
                        }
                        else if (errorMessage.Contains("yetki") || errorMessage.Contains("unauthorized"))
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        }
                        else if (errorMessage.Contains("validation"))
                        {
                            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                        }

                        _logger.LogWarning($"İşlem başarısız: {errorMessage}");
                    }
                }

                context.Response.Headers.Remove("Content-Length");

                memoryStream.Seek(0, SeekOrigin.Begin);

                await memoryStream.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred outside of Middleware");

                context.Response.Headers.Remove("Content-Length");

                context.Response.Body = originalBodyStream;

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var result = Result.Failure("An unexpected error occurred: " + ex.Message);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}