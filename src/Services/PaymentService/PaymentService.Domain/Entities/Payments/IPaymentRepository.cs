namespace PaymentService.Domain.Entities.Payments
{
	public interface IPaymentRepository
	{
		Payment Create(Payment payment);
	}
}
