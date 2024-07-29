using Microsoft.EntityFrameworkCore;
using PaymentService.Domain.Entities.Payments;

namespace PaymentService.Infrastructure.Persistence.Repositories
{
	internal class PaymentRepository(AppDbContext context) : IPaymentRepository
	{
		private readonly DbSet<Payment> cards = context.Set<Payment>();
	}
}
