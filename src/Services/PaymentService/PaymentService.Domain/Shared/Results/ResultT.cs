using PaymentService.Domain.Shared.Errors;

namespace PaymentService.Domain.Shared.Results
{
	public class Result<TValue> : Result
	{
		private readonly TValue? _value;

		public TValue? Value => _value;

		protected internal Result(TValue? value, bool isSuccess, Error? error) : base(isSuccess, error)
		{
			_value = value;
		}

		protected internal Result(TValue? value, bool isSuccess, IEnumerable<Error> errors) : base(isSuccess, errors)
		{
			_value = value;
		}

		public static implicit operator Result<TValue>(TValue? value) => Create(value);

		public static implicit operator TValue?(Result<TValue> result) => result.Value;

		public Result<TValue> Ensure(Func<TValue?, bool> predicate, Error error)
		{
			if (predicate == null) throw new ArgumentNullException(nameof(predicate));

			if (error == null) throw new ArgumentNullException(nameof(error));

			if (predicate.Invoke(_value) == false)
			{
				_status = false;
				_errors.Add(error);
			}

			return this;
		}

		public Result<TValue> EnsureOnSuccess(Func<TValue?, bool> predicate, Error error)
		{
			if (IsSuccess)
				return Ensure(predicate, error);
			else
				return this;
		}

		public static async Task<Result<TValue>> FirstFailureOrSuccess(params Func<Task<Result<TValue>>>[] results)
		{
			Result<TValue>? result = null;

			foreach (Func<Task<Result<TValue>>> resultTask in results)
			{
				result = await resultTask();

				if (result.IsFailure)
				{
					return result;
				}
			}

			return result;
		}
	}
}
