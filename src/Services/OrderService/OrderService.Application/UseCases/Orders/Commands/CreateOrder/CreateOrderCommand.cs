using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public record CreateOrderResult(Guid Id);

public record CreateOrderCommand(
    Guid CustomerId,
    Guid? CartId,
    AddressDto ShippingAddress,
    List<OrderItemDto> Items)
    : ICommand<CreateOrderResult>;

