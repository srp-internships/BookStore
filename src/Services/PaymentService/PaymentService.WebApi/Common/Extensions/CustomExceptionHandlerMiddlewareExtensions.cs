using PaymentService.WebApi.Middlewares;

namespace PaymentService.WebApi.Common.Extensions
{
	public static class CustomExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandlerMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
		}
	}
}
