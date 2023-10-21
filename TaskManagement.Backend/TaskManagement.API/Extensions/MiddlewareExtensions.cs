using TaskManagement.API.Middlewares;

namespace TaskManagement.API.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlboalExecption(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        }
    }
}
