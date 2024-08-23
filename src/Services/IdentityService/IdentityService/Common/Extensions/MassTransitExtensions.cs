using IdentityService.Common.Configurations.RabbitMq;
using MassTransit;
using Microsoft.Extensions.Options;

namespace IdentityService.Common.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>()
                .AddMassTransit(busConfigurator =>
                {
                    busConfigurator.SetEndpointNameFormatter(
                        new KebabCaseEndpointNameFormatter(prefix: "ids", includeNamespace: false));

                    busConfigurator.AddConsumers(typeof(Program).Assembly);

                    busConfigurator.UsingRabbitMq((context, configurator) =>
                    {
                        var rabbitMqOptions = context.GetRequiredService<IOptions<RabbitMqOptions>>().Value;

                        configurator.Host(rabbitMqOptions.Host, rabbitMqOptions.VirtualHost, h =>
                        {
                            h.Username(rabbitMqOptions.Username);
                            h.Password(rabbitMqOptions.Password);
                        });

                        configurator.ConfigureEndpoints(context);
                    });
                });

            return services;
        }
    }
}
