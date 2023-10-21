using System.Net;
using System.Text.Json;
using TaskManagement.Core.Constants;
using TaskManagement.Core.Responses;

namespace TaskManagement.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //we can configure multile catch blocks here and return specific error messages
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = new ErrorResponse 
                { 
                    Code = ErrorConstants.GenericError,
                    Message = ex.Message
                };
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, response, (int)HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorResponse errorResponse, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
