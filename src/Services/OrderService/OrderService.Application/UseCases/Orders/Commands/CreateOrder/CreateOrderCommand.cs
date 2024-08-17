using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    Guid? CartId,
    AddressDto ShippingAddress,
    List<OrderItemDto> Items)
    : ICommand<Guid>;

