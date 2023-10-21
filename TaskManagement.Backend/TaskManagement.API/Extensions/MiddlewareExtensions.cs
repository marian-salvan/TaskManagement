using TaskManagement.API.Middlewares;

namespace TaskManagement.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlboalExeception(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
