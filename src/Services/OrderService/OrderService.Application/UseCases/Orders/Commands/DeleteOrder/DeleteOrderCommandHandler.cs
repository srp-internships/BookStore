using OrderService.Application.Common.Exceptions;
using OrderService.Application.Common.Interfaces.Data;

namespace OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteOrderCommand, bool>
{

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.GetAsync(request.OrderId, cancellationToken);

        if (order == null)
        {
            throw new NotFoundException(nameof(order), request.OrderId);
        }
        else
        {
            await unitOfWork.OrderRepository.DeleteAsync(order);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        return true;
    }
}
