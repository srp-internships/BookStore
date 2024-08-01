using SendGrid.Helpers.Errors.Model;
using System.Net;

namespace ReviewService.WebApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var message = "Internal server error";

                if (ex is NotFoundException) 
                {
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Resource not found";
                }

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = statusCode;

                var response = new
                {
                    errorMessage = message,
                    stackTrace = _env.IsDevelopment() ? ex.StackTrace : null
                };

                _logger.LogError(ex, "An unhandled exception has occurred.");
                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
