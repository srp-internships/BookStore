using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PaymentService.WebApi.Common.Extensions
{
	public static class AuthenticationByJwtTokenExtensions
	{
		public static IServiceCollection AddAuthenticationByJwtToken(this IServiceCollection services)
		{
			// Adding Authentication
			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				// TODO setup to use oauth2.0 + Identity server after it has done
			});

			return services;
		}
	}
}
