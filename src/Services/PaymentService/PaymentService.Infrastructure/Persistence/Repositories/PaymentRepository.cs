using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities.Payments;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
	internal class PaymentRepository(AppDbContext context) : IPaymentRepository
	{
		private readonly DbSet<Payment> payments = context.Set<Payment>();

		public Payment Create(Payment payment)
		{
			payments.Add(payment);
			return payment;
		}
	}
}
