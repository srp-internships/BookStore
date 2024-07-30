using PaymentService.Domain.Shared.Errors;

namespace PaymentService.Domain.Shared.Results
{
	public class Result
	{
		protected bool _status;
		protected readonly List<Error> _errors;

		public bool IsSuccess => _status;

		public bool IsFailure => !IsSuccess;

		public IReadOnlyCollection<Error> Errors => _errors;

		protected internal Result(bool isSuccess, Error? error)
		{
			if (isSuccess == true && error != Error.None)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			if (isSuccess == false && (error == null || error == Error.None))
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(error)}'");

			_status = isSuccess;
			_errors = isSuccess == true ? new() : new() { error! };
		}

		protected internal Result(bool isSuccess, IEnumerable<Error> errors)
		{
			ArgumentNullException.ThrowIfNull(errors, nameof(errors));

			if (isSuccess == true && errors.Any() == true)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			if (isSuccess == false && errors.Any() == false)
				throw new ArgumentException($"Inappropriate values of '{nameof(isSuccess)}' and '{nameof(errors)}'");

			_status = isSuccess;
			_errors = errors.ToList();
		}

		public static Result Success() => new(true, Error.None);

		public static Result<TValue> Success<TValue>(TValue? value) => new(value, true, Error.None);

		public static Result Failure() => new(false, Error.Default);

		public static Result<TValue> Failure<TValue>(TValue? value) => new(value, false, Error.Default);

		public static Result Failure(Error error) => new(false, error);

		public static Result<TValue> Failure<TValue>(TValue? value, Error error) => new(value, false, error);

		public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);

		public static Result Failure(IEnumerable<Error> errors) => new(false, errors);

		public static Result<TValue> Failure<TValue>(IEnumerable<Error> errors) => new(default, false, errors);

		public static Result<TValue> Failure<TValue>(TValue? value, IEnumerable<Error> errors) => new(value, false, errors);

		public static Result Create(bool condition) => condition ? Success() : Failure(Error.ConditionNotMet);

		public static Result<TValue> Create<TValue>(TValue? value) => value is not null
																				? Success(value)
																				: Failure<TValue>(Error.NullValue);

		public Result Ensure(Func<bool> predicate, Error error)
		{
			ArgumentNullException.ThrowIfNull(predicate);
			ArgumentNullException.ThrowIfNull(error);

			if (predicate.Invoke() == false)
			{
				_status = false;
				_errors.Add(error);
			}

			return this;
		}

		public Result EnsureOnSuccess(Func<bool> predicate, Error error)
		{
			if (IsSuccess)
				return Ensure(predicate, error);
			else
				return this;
		}

		public static async Task<Result> FirstFailureOrSuccess(params Func<Task<Result>>[] results)
		{
			foreach (Func<Task<Result>> resultTask in results)
			{
				Result result = await resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return Success();
		}

		public static Result FirstFailureOrSuccess(params Func<Result>[] results)
		{
			foreach (Func<Result> resultTask in results)
			{
				Result result = resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return Success();
		}

		public static Result Combine(params Result[] results)
		{
			if (results == null || results.Length == 0)
				throw new ArgumentNullException(nameof(results));

			if (results.Any(i => i.IsFailure))
				return Failure(results.SelectMany(r => r.Errors).Distinct());

			return Success();
		}
	}
}
