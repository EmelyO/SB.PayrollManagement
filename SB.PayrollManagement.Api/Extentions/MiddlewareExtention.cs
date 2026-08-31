using SB.PayrollManagement.Api.Middlewares;

namespace SB.PayrollManagement.Api.Extentions
{
    public static class MiddlewareExtention
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
