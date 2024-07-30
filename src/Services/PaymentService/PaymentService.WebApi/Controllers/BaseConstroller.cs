using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Domain.Shared.Errors;
using PaymentService.Domain.Shared.Results;

namespace PaymentService.WebApi.Controllers
{
	[ApiController]
	public abstract class BaseController : ControllerBase
	{
		private readonly static Error BadRequestError = new("BadRequest",
															"The request parameters were incorrect or incomplete.");

		private IMediator mediator;
		protected IMediator Mediator => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

		private IMapper mapper;
		protected IMapper Mapper => mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();

		protected ActionResult HandleFailure(Result result) =>
			result switch
			{
				{ IsSuccess: true } =>
					throw new InvalidOperationException("This method can't be invoked for a success result."),
				var notFoundResult when notFoundResult.Errors.Count == 1
									 && notFoundResult.Errors.ElementAt(0) is NotFoundError notFound =>
					NotFound(CreateProblemDetails("Not Found", StatusCodes.Status404NotFound, notFound)),
				var conflictResult when conflictResult.Errors.Count == 1
									  && conflictResult.Errors.ElementAt(0) is ConflictError conflict =>
					Conflict(CreateProblemDetails("Conflict", StatusCodes.Status409Conflict, conflict)),
				var badRequest =>
					BadRequest(CreateProblemDetails(
						"Bad Request",
						StatusCodes.Status400BadRequest,
						BadRequestError,
						badRequest.Errors))
			};

		private static ProblemDetails CreateProblemDetails(string title, int status, Error error, IEnumerable<Error>? errors = null) =>
			new()
			{
				Title = title,
				Type = error.Code,
				Detail = error.Message,
				Status = status,
				Extensions = { { nameof(errors), errors } }
			};
	}
}
