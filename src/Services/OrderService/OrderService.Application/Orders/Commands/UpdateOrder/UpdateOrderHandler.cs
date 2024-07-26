using OrderService.Application.Orders.Exceptions;

namespace OrderService.Application.Orders.Commands.UpdateOrder;

//public class UpdateOrderHandler(IApplicationDbContext dbContext)
//    : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
//{
//    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
//    {
//        //Update Order entity from command object
//        //save to database
//        //return result

//        var orderId = (command.Order.Id);
//        var order = await dbContext.Orders
//            .FindAsync([orderId], cancellationToken: cancellationToken);

//        if (order is null)
//        {
//            throw new OrderNotFoundException(command.Order.Id);
//        }

//    //    UpdateOrderWithNewValues(order, command.Order);

//        dbContext.Orders.Update(order);
//        await dbContext.SaveChangesAsync(cancellationToken);

//        return new UpdateOrderResult(true);
//    }

//    //public void UpdateOrderWithNewValues(Order order, OrderDto orderDto)
//    //{
//    //    var updatedShippingAddress = Address.Of(orderDto.ShippingAddress.FirstName, orderDto.ShippingAddress.LastName, orderDto.ShippingAddress.EmailAddress, orderDto.ShippingAddress.Country, orderDto.ShippingAddress.State, orderDto.ShippingAddress.ZipCode);
//    //    var updatedPayment = Payment.(orderDto.Payment.CardName, orderDto.Payment.CardNumber, orderDto.Payment.Expiration, orderDto.Payment.Cvv);

//    //    order.Update(
//    //        shippingAddress: updatedShippingAddress,
//    //        payment: updatedPayment,
//    //        status: orderDto.Status);
//    //}
//}

