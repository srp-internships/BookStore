using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Mappers;
using OrderService.Domain.Entities;
using OrderService.IntegrationEvents;
using PaymentService.IntegrationEvents;
using OrderStatus = OrderService.IntegrationEvents.OrderStatus;

namespace OrderService.Infrastructure.Consumers;

public class PaymentStatusUpdatedConsumer : IConsumer<PaymentRequestProcessedEvent>
{
    private readonly ILogger<PaymentStatusUpdatedConsumer> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ApplicationDbContext _context;

    public PaymentStatusUpdatedConsumer(ILogger<PaymentStatusUpdatedConsumer> logger,
        IPublishEndpoint publishEndpoint,
        ApplicationDbContext dbContext)
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _context = dbContext;
    }

    public async Task Consume(ConsumeContext<PaymentRequestProcessedEvent> context)
    {
        var message = context.Message;

        var paymentStatus = PaymentStatusMapper.ToDomainPaymentStatus(message.PaymentStatus);

        var payment = await _context.Payments.FindAsync(message.OrderId);

        if (payment == null)
        {
            payment = new Payment
            {
                OrderId = message.OrderId,
                PaymentStatus = paymentStatus,
                Message = message.Message?.ToString(),
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        _logger.LogInformation($"Processing payment {payment.PaymentStatus}");
        await Task.Delay(3000);
        payment.PaymentStatus = Domain.Enums.PaymentStatus.Succeeded;

        await _context.SaveChangesAsync();

        await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(payment.OrderId, OrderStatus.ShipmentProcessing));
        _logger.LogInformation($"Order {message.OrderId} has been paid successfully and processed shipment .");
    }

}