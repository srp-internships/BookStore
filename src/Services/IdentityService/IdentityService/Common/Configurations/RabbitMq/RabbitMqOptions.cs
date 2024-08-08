namespace IdentityService.Common.Configurations.RabbitMq
{
	internal sealed class RabbitMqOptions
	{
		public string Host { get; init; }

		public string VirtualHost { get; init; }

		public string Username { get; init; }

		public string Password { get; init; }
	}
}
