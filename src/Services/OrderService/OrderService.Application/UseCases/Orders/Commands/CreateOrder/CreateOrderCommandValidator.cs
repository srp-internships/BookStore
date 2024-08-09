namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotNull().WithMessage("CustomerId is required");
        RuleFor(x => x.Items).NotEmpty().WithMessage("OrderItems should not be empty");
    }
}