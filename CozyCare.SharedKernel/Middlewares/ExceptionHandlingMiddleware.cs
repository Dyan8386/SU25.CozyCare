using CozyCare.SharedKernel.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CozyCare.SharedKernel.Middlewares
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
            catch (BaseException.ErrorException ex)
            {
                _logger.LogWarning("[Handled] ErrorException: {Message}", ex.ErrorDetail?.ErrorMessage?.ToString());
                await WriteJsonResponseAsync(context, ex.StatusCode, ex.ErrorDetail);
            }
            catch (BaseException.CoreException ex)
            {
                _logger.LogWarning("[Handled] CoreException: {Message}", ex.Message);
                await WriteJsonResponseAsync(context, ex.StatusCode, new BaseException.ErrorDetail
                {
                    ErrorCode = ex.Code,
                    ErrorMessage = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[Unhandled] Internal Server Error");
                await WriteJsonResponseAsync(context, (int)HttpStatusCode.InternalServerError, new BaseException.ErrorDetail
                {
                    ErrorCode = "server_error",
                    ErrorMessage = "Internal server error"
                });
            }
        }

        private static async Task WriteJsonResponseAsync(HttpContext context, int statusCode, BaseException.ErrorDetail errorDetail)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var json = JsonSerializer.Serialize(new
            {
                error = errorDetail.ErrorCode,
                message = errorDetail.ErrorMessage
            });

            await context.Response.WriteAsync(json);
        }
    }
}
