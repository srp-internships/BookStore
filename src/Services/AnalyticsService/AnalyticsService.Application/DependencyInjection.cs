using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(AssemblyReference.Assembly);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
           
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
