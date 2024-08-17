namespace PaymentService.Domain.Shared.Errors
{
	public sealed class ConflictError : Error
	{
		public ConflictError(string code, string message)
			: base(code, message)
		{
		}
	}
}
