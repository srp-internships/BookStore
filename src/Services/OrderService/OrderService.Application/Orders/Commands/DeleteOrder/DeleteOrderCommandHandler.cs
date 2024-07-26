namespace OrderService.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IOrderRepository orderRepository) 
    : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId, cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(request.OrderId);
        }
        await _orderRepository.DeleteAsync(order, cancellationToken);
        return new DeleteOrderResult(true);
    }
}
