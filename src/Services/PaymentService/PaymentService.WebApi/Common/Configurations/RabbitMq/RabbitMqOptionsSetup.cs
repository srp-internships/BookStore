using Microsoft.Extensions.Options;

namespace PaymentService.WebApi.Common.Configurations.RabbitMq
{
	internal sealed class RabbitMqOptionsSetup(IConfiguration configuration) : IConfigureOptions<RabbitMqOptions>
	{
		private const string ConfigurationSectionName = "RabbitMQ";

		public void Configure(RabbitMqOptions options) => configuration.GetSection(ConfigurationSectionName).Bind(options);
	}
}
