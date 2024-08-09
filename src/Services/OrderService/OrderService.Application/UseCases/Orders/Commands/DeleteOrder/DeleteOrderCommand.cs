namespace OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId)
    : ICommand<bool>;

