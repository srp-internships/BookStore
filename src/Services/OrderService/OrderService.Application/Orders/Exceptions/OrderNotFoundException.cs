using OrderService.Application.Common.Exceptions;

namespace OrderService.Application.Orders.Exceptions;

public class OrderNotFoundException : NotFoundException
{
    public OrderNotFoundException(Guid id) : base("Order", id)
    {
    }
}
