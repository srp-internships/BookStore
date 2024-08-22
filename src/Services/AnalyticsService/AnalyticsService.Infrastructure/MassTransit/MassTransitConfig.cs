using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MassTransit;
using AnalyticsService.Infrastructure.Consumers;

namespace AnalyticsService.Infrastructure.MassTransit
{
    public static class MassTransitConfig
    {
        public static void AddMassTransitConfiguration(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<BooksPurchasedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");
                    cfg.ReceiveEndpoint("books-purchased-event", e =>
                    {
                        e.ConfigureConsumer<BooksPurchasedConsumer>(context);
                    });
                });
            });

            //services.AddMassTransitHostedService();
        }
    }
}
