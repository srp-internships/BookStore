namespace OrderService.Application.UseCases.DTOs;

public record OrderDto(
    Guid OrderId,
    Guid CustomerId,
    Guid? CartId,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> Items,
    decimal TotalPrice);
