using OrderService.Application.Common.Interfaces.Data;
using OrderService.Application.UseCases.Orders.Exceptions;

namespace OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteOrderCommand, bool>
{

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetAsync(request.OrderId, cancellationToken);

        if (order == null)
            throw new OrderNotFoundException(request.OrderId);

        await unitOfWork.OrderRepository.DeleteAsync(request.OrderId);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
