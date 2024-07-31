namespace OrderService.Application.UseCases.DTOs;

public record OrderDto(
    Guid CustomerId,
    Guid? CartId,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> Items,
    decimal TotalPrice);
