namespace PaymentService.Application.Orders
{
	public static class OrderErrors
	{
		public static Error CustomerDoesNotHaveCard()
			=> new(
				"Order.CustomerDoesNotHaveCard",
				"Unable to process payment. The customer has not added a card.");

		public static Error SellerDoesNotHaveCard()
			=> new(
				"Order.SellerDoesNotHaveCard",
				$"Unable to process payment. The seller has not added a card.");
	}
}
