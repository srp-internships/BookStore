using OrderService.Domain.Enums;

namespace OrderService.Application.Dtos;

public record OrderDto(
    Guid CustomerId,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> Items);
