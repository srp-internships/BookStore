using Microsoft.Extensions.Options;
using PaymentService.WebApi.Common.Configurations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PaymentService.WebApi.Common.Extensions
{
	public static class SwaggerExtensions
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen();
			services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

			return services;
		}
	}
}
