using FluentValidation.Results;

namespace PaymentService.Application.Common.Exceptions
{
	public class FluentValidationException : Exception
	{
		public IDictionary<string, string[]> Errors { get; }

		public FluentValidationException(IEnumerable<ValidationFailure> failures,
								   string? source,
								   Exception? innerException = null) : this(failures,
													"One or more validation errors occurred.",
													source,
													innerException)
		{
		}

		public FluentValidationException(IEnumerable<ValidationFailure> failures,
								   string? message,
								   string? source,
								   Exception? innerException = null) : base(
													message,
													innerException)
		{
			ArgumentNullException.ThrowIfNull(failures);

			Source = source;

			Errors = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage)
				.ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
		}
	}
}
