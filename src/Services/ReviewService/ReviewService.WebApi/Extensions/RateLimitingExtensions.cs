using Microsoft.AspNetCore.Builder;
using ReviewService.WebApi.Middlewares;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ReviewService.WebApi.Extensions
{
    public static class RateLimitingExtensions
    {
        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<RateLimitingMiddleware>();  
        }
    }
}
