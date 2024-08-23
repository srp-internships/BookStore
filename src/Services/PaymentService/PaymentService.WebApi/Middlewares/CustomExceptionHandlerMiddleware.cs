using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentService.Application.Common.Exceptions;
using Serilog;

namespace PaymentService.WebApi.Middlewares
{
	public class CustomExceptionHandlerMiddleware
	{
		private readonly RequestDelegate next;
		private readonly IDictionary<Type, Func<HttpContext, Exception, Task>> exceptionHandlers;

		public CustomExceptionHandlerMiddleware(RequestDelegate next)
		{
			this.next = next;

			// Register known exception types and handlers here.
			exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
			{
				{ typeof(FluentValidationException), HandleFluentValidationException },
			};
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			Type type = ex.GetType();

			if (exceptionHandlers.ContainsKey(type))
				await exceptionHandlers[type].Invoke(context, ex);
			else
				await HandleUnknownException(context, ex);
		}

		#region Default exception handlers

		private async Task HandleUnknownException(HttpContext context, Exception ex)
		{
			context.Response.ContentType = "application/problem+json";
			context.Response.StatusCode = StatusCodes.Status500InternalServerError;

			var problemDetails = new ProblemDetails
			{
				Status = StatusCodes.Status500InternalServerError,
				Title = "Internal Server Error",
				Detail = "Internal server error. Connect to support team.",
				Instance = context.Request.Path
			};

			await context.Response.WriteAsJsonAsync(problemDetails);

			Log.Error("Unknown exception caught. Details: {@ex}", ex);
		}

		#endregion

		#region Registered exceptions handlers

		private async Task HandleFluentValidationException(HttpContext context, Exception ex)
		{
			if (ex is FluentValidationException fvex)
			{
				context.Response.ContentType = "application/problem+json";
				context.Response.StatusCode = StatusCodes.Status400BadRequest;

				var problemDetails = new ValidationProblemDetails
				{
					Status = StatusCodes.Status400BadRequest,
					Title = "Validation Error",
					Detail = fvex.Message,
					Instance = context.Request.Path,
					Errors = fvex.Errors,
				};

				await context.Response.WriteAsJsonAsync(problemDetails);

				Log.Information("Fluent validation exception caught. Exception thrown in '{Source}' with message '{Message}'\nDetails: {Details}\nException object: {Exception}",
					fvex.Source,
					fvex.Message,
					JsonConvert.SerializeObject(fvex.Errors),
					fvex);
			}
		}

		// add your custom exception handlers here, don't forget to add logging

		#endregion
	}
}
