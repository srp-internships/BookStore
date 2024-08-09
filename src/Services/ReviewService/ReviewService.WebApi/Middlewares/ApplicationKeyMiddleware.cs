namespace ReviewService.WebApi.Middlewares
{
    public class ApplicationKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApplicationKeyMiddleware> _logger;
        private const string AppKeyHeaderName = "AppKey";
        private readonly string _appKey;
        public ApplicationKeyMiddleware(RequestDelegate next, ILogger<ApplicationKeyMiddleware> logger, string appKey)
        {
            _next = next;
            _logger = logger;
            _appKey = appKey;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            //if (httpContext.Request.Headers.TryGetValue(AppKeyHeaderName, out var key) && key.FirstOrDefault() == _appKey)
            //{
                await _next(httpContext);
            //}
            //else
            //{
            //    _logger.LogWarning("Invalid or missing AppKey");
            //    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            //    await httpContext.Response.WriteAsync("Forbidden: Invalid or missing AppKey");
            //}
            
        }
    }
}
