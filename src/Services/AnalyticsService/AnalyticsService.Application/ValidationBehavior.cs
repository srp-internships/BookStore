using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalyticsService.Application
{
    internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
                                            : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
    {
        public async Task<TResponse> Handle(TRequest request,
                                              RequestHandlerDelegate<TResponse> next,
                                              CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Count != 0)
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Count != 0)
                    // throw new FluentValidationException(failures, string.Empty, GetType().FullName!);
                    throw new Exception("");
            }

            return await next();
        }
    }
}
