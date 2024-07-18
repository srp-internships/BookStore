using OrderService.Domain.Enums;

namespace OrderService.Application.Dtos;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    AddressDto ShippingAddress,
    AddressDto BillingAddress,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);
