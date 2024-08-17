namespace PaymentService.WebApi.Common
{
	public class WebApiSettings
	{
		public const string ConfigurationSection = "WebApi";

		public string LogFilesDirectory { get; set; } = string.Empty;
		public bool EnableSwaggerUI { get; set; } = false;
	}
}
