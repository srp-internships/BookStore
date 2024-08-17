﻿using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog;

namespace PaymentService.WebApi.Common.Extensions
{
	public static class SerilogExtensions
	{
		public static IServiceCollection AddSerilogStuff(this IServiceCollection services)
		{
			var appSettings = services.BuildServiceProvider().GetRequiredService<IOptions<WebApiSettings>>().Value;

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Is(LogEventLevel.Warning)
				.WriteTo.File(Path.Combine(appSettings.LogFilesDirectory, "ProgramLog-.txt"),
								rollingInterval: RollingInterval.Day)
				.WriteTo.Console()
				.CreateLogger();

			services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(Log.Logger));

			return services;
		}
	}
}
