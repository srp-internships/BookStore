using MassTransit;
using Microsoft.Extensions.Options;
using PaymentService.Application.Orders.Events;
using PaymentService.WebApi.Common.Configurations.RabbitMq;

namespace PaymentService.WebApi.Common.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMqOptionsSetup>()
                .AddMassTransit(busConfigurator =>
                {
                    busConfigurator.SetKebabCaseEndpointNameFormatter();

                    //busConfigurator.AddConsumers(typeof(Application.DependencyInjection).Assembly);
                    busConfigurator.AddConsumer<OrderProcessedIntegrationEventConsumer>();

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
