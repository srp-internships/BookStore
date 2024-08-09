using OrderService.Application.Common.Interfaces.Data;

namespace OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
    : ICommandHandler<DeleteOrderCommand, bool>
{

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.OrderRepository.DeleteAsync(request.OrderId);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
