using Microsoft.Extensions.Options;

namespace IdentityService.Common.Configurations.RabbitMq
{
	internal sealed class RabbitMqOptionsSetup(IConfiguration configuration)
		: IConfigureOptions<RabbitMqOptions>
	{
		private const string ConfigurationSectionName = "RabbitMQ";

		public void Configure(RabbitMqOptions options) 
			=> configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
