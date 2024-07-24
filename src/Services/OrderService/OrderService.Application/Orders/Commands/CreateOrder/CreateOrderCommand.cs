using OrderService.Domain.Enums;

namespace OrderService.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    Guid CartId,
    AddressDto ShippingAddress,
    OrderStatus Status,
    List<OrderItemDto> Items)
    : ICommand<CreateOrderResult>;

public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}