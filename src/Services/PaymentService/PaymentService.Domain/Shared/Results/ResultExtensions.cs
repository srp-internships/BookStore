﻿using PaymentService.Domain.Shared.Errors;

namespace PaymentService.Domain.Shared.Results
{
	public static class ResultExtensions
	{
		public static Result<TOut> Map<TOut>(this Result result, Func<TOut> func) =>
			result.IsSuccess ? func() : Result.Failure<TOut>(result.Errors);

		public static async Task<Result<TOut>> Map<TOut>(this Task<Result> resultTask, Func<TOut> func) =>
			(await resultTask).Map(func);

		public static Result<TOut> Map<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Errors);

		public static async Task<Result<TOut>> Map<TIn, TOut>(this Task<Result<TIn>> resultTask, Func<TIn, TOut> func) =>
			(await resultTask).Map(func);

		public static Result<TIn> MapFailure<TIn>(this Result<TIn> result, Func<Error> func) =>
			result.IsSuccess ? result : Result.Failure<TIn>(func());

		public static async Task<Result<TIn>> MapFailure<TIn>(this Task<Result<TIn>> resultTask, Func<Error> func) =>
			(await resultTask).MapFailure(func);

		public static Result Bind<TIn>(this Result<TIn> result, Func<TIn, Result> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure(result.Errors);

		public static async Task<Result> Bind<TIn>(this Task<Result<TIn>> result, Func<TIn, Result> func) =>
			(await result).Bind(func);

		public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure(result.Errors);

		public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> func) =>
			result.IsSuccess ? func(result.Value) : Result.Failure<TOut>(result.Errors);

		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func(result.Value) : Result.Failure<TOut>(result.Errors);

		public static async Task<Result<TOut>> Bind<TOut>(this Result result, Func<Task<Result<TOut>>> func) =>
			result.IsSuccess ? await func() : Result.Failure<TOut>(result.Errors);

		public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> func) =>
			result.IsSuccess ? func() : Result.Failure<TOut>(result.Errors);

		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
																Func<TIn, Result<TOut>> func) =>
			(await resultTask).Bind(func);

		public static async Task<Result<TOut>> Bind<TIn, TOut>(this Task<Result<TIn>> resultTask,
																Func<TIn,
																Task<Result<TOut>>> func) =>
			await (await resultTask).Bind(func);

		public static async Task<Result> Tap(this Result result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		public static async Task<Result> Tap(this Task<Result> resultTask, Func<Task> func) =>
			await (await resultTask).Tap(func);

		public static Result<TIn> Tap<TIn>(this Result<TIn> result, Action<TIn> action)
		{
			if (result.IsSuccess)
			{
				action(result.Value);
			}

			return result;
		}

		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Func<Task> func) =>
			await (await resultTask).Tap(func);

		public static async Task<Result<TIn>> Tap<TIn>(this Task<Result<TIn>> resultTask, Action<TIn> action) =>
			(await resultTask).Tap(action);

		public static async Task<Result<TIn>> Tap<TIn>(this Result<TIn> result, Func<Task> func)
		{
			if (result.IsSuccess)
			{
				await func();
			}

			return result;
		}

		public static async Task<Result> OnFailure(this Task<Result> resultTask, Action<IEnumerable<Error>> action)
		{
			Result result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		public static async Task<Result> OnFailure(this Task<Result> resultTask, Func<IEnumerable<Error>, Task> action)
		{
			Result result = await resultTask;

			if (result.IsFailure)
			{
				await action(result.Errors);
			}

			return result;
		}

		public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask,
															Action<IEnumerable<Error>> action)
		{
			Result<TIn> result = await resultTask;

			if (result.IsFailure)
			{
				action(result.Errors);
			}

			return result;
		}

		public static async Task<Result<TIn>> OnFailure<TIn>(this Task<Result<TIn>> resultTask,
															Func<IEnumerable<Error>, Task> action)
		{
			Result<TIn> result = await resultTask;

			if (result.IsFailure)
			{
				await action(result.Errors);
			}

			return result;
		}

		public static Result<TIn> Filter<TIn>(this Result<TIn> result, Func<TIn, bool> predicate)
		{
			if (result.IsFailure)
			{
				return result;
			}

			return predicate(result.Value) ? result : Result.Failure<TIn>(Error.ConditionNotMet);
		}

		public static Task<Result> ToTask(this Result result) => Task.FromResult(result);

		public static Task<Result<TValue?>> ToTask<TValue>(this Result<TValue?> result) => Task.FromResult(result);
	}
}
