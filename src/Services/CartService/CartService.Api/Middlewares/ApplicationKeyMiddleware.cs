using Microsoft.Extensions.Primitives;

namespace CartService.Api.Middlewares
{
    public class ApplicationKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApplicationKeyMiddleware> _logger;

        public ApplicationKeyMiddleware(RequestDelegate next, ILogger<ApplicationKeyMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);  
        }
    }
}
