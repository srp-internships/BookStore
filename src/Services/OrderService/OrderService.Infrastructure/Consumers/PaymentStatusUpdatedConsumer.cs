using MassTransit;
using Microsoft.Extensions.Logging;
using OrderService.Application.Common.Extensions;
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

        var paymentStatus = PaymentStatusMapperExtensions.ToDomainPaymentStatus(message.PaymentStatus);

        var payment = await _context.Payments.FindAsync(message.OrderId);

        if (payment == null)
        {
            payment = new Payment
            {
                OrderId = message.OrderId,
                PaymentStatus = paymentStatus,
                Message = message.Message?.ToString()
            };

            _context.Payments.Add(payment);
        }
        else
        {
            payment.PaymentStatus = paymentStatus;
            payment.Message = message.Message?.ToString();
        }

        await _context.SaveChangesAsync();

        var order = await _context.Orders.FindAsync(message.OrderId);

        if (order != null)
        {
            if (paymentStatus == Domain.Enums.PaymentStatus.Failed)
            {
                order.Status = Domain.Enums.OrderStatus.Failed;
                await _context.SaveChangesAsync();

                await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(payment.OrderId, OrderStatus.Failed));
                _logger.LogInformation($"Order {message.OrderId} payment failed and updated to failed status.");
            }
            else if (paymentStatus == Domain.Enums.PaymentStatus.Succeeded)
            {
                order.Status = Domain.Enums.OrderStatus.ShipmentProcessing;
                await _context.SaveChangesAsync();

                await _publishEndpoint.Publish(new OrderStatusUpdatedIntegrationEvent(payment.OrderId, OrderStatus.ShipmentProcessing));
                _logger.LogInformation($"Order {message.OrderId} has been paid successfully and processed to shipment.");
            }
            else
            {
                _logger.LogInformation($"Order {message.OrderId} payment status is pending.");
            }
        }
        else
        {
            _logger.LogWarning($"Order {message.OrderId} not found.");
        }
    }
}
