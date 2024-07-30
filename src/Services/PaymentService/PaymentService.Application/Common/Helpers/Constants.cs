namespace PaymentService.Application.Common.Helpers
{
	public static class Constants
	{
		public const string CardNumberRegExPattern = @"^4[0-9]{12}(?:[0-9]{3})?$";
		public const string CardCvcRegExPattern = @"^\d{3}$";
	}
}
