using ReviewService.WebApi.Middlewares;

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
