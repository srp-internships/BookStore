using MassTransit;
using OrderService.IntegrationEvents;
using PaymentService.Application.Common.Inbox;
using PaymentService.Domain;
using PaymentService.Domain.Entities.Cards;
using PaymentService.Domain.Entities.Payments;
using PaymentService.IntegrationEvents;

namespace PaymentService.Application.Orders.Events
{
    public class OrderProcessedIntegrationEventConsumer(
        IInboxRepository inboxRepository,
        IPaymentRepository paymentRepository,
        ICardRepository cardRepository,
        IUnitOfWork db,
        IBus bus)
        : IConsumer<OrderProcessedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<OrderProcessedIntegrationEvent> context)
        {
            var inboxMessage = new InboxMessage(
                                        Guid.NewGuid(),
                                        DateTime.UtcNow,
                                        typeof(OrderProcessedIntegrationEvent).Name,
                                        JsonConvert.SerializeObject(context.Message,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.All
                                                        }));

            var payment = await CreatePayment(context.Message);

            if (payment!.IsSuccess)
            {
                inboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                inboxRepository.Create(inboxMessage);

                await ProcessPayment(payment!.Value);

                payment.Value.ProcessedAtUtc = DateTime.UtcNow;
                paymentRepository.Create(payment.Value);

                await db.SaveChangesAsync();

                await bus.Publish(CreatePaymentEvent(
                                    context.Message.OrderId,
                                    PaymentStatus.Succeeded,
                                    new("Order.Succeeded", "Payment successfully processed.")));
            }
            else
            {
                inboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                inboxMessage.Error = JsonConvert.SerializeObject(payment.Errors);
                inboxRepository.Create(inboxMessage);

                await db.SaveChangesAsync();

                await bus.Publish(CreatePaymentEvent(
                                        context.Message.OrderId,
                                        PaymentStatus.Failed,
                                        payment.Errors
                                                .Select(i => new PaymentMessage(i.Code, i.Message))
                                                .First()));
            }
        }

        private async Task<Result<Payment>?> CreatePayment(OrderProcessedIntegrationEvent _event)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = _event.OrderId,
                RequestedAtUtc = DateTime.UtcNow,
                ProcessedAtUtc = null,
                Transaction = [],
            };

            var customerCard = await cardRepository.GetByUserIdAsync(_event.CustomerId);
            if (customerCard is null)
                return Result.Failure<Payment>(OrderErrors.CustomerDoesNotHaveCard());

            var customerTransaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = _event.Items.Sum(i => i.Quantity * i.Price),
                Card = customerCard,
                Type = TransactionType.Withdrawal,
            };

            payment.Transaction.Add(customerTransaction);

            var sellersIds = _event.Items.Select(i => i.SellerId).Distinct();
            var sellersCards = await cardRepository.GetByUserIdsAsync(sellersIds);
            if (sellersCards?.Distinct().Count() != sellersIds.Count())
                return Result.Failure<Payment>(OrderErrors.SellerDoesNotHaveCard());

            payment.Transaction.AddRange(
                _event.Items.GroupBy(i => i.SellerId).Select(g => new
                {
                    sellerId = g.Key,
                    replenishment = g.Sum(o => o.Quantity * o.Price)
                })
                .Select(i => new Transaction
                {
                    Id = Guid.NewGuid(),
                    Amount = i.replenishment,
                    Card = sellersCards.First(m => m.UserId == i.sellerId),
                    Type = TransactionType.Replenishment,
                }));

            return Result.Success(payment);
        }

        private Task ProcessPayment(Payment payment)
        {
            // Imitating payment process.
            Log.Logger.Warning("Payment processed: {orderId}", payment.OrderId);
            return Task.Delay(1000);
        }

        private PaymentRequestProcessedEvent CreatePaymentEvent(Guid orderId,
                                                                PaymentStatus paymentStatus,
                                                                PaymentMessage? message = null)
            => new(Guid.NewGuid(),
                    DateTime.UtcNow,
                    orderId,
                    paymentStatus,
                    message);
    }
}
