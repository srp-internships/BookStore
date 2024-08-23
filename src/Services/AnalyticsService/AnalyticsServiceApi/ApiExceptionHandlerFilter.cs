using AnalyticService.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AnalyticsServiceApi;

public class ApiExceptionHandlerFilter : IAsyncExceptionFilter
{

    public Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            context.Result = new NotFoundObjectResult(context.Exception.Message);
        }
        else
        {
            
        }

        return Task.CompletedTask;
    }
}