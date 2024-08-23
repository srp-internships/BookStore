namespace OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(t => t.OrderId).NotEmpty().WithMessage("OderId is required");
    }
}
