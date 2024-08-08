using OrderService.Application.Common.Interfaces.Data;
using OrderService.IntegrationEvents;
using Address = OrderService.IntegrationEvents.Address;
using OrderItem = OrderService.IntegrationEvents.OrderItem;
using OrderStatus = OrderService.IntegrationEvents.OrderStatus;

namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IPublishEndpoint publishEndpoint,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await unitOfWork.OrderRepository.CreateAsync(mapper.Map<Order>(command), cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        var orderProcessedEvent = new OrderProcessedIntegrationEvent(
        OrderId: order.Id,
        CustomerId: command.CustomerId,
        Status: OrderStatus.PaymentProcessing,
        ShippingAddress: new Address(
            FirstName: command.ShippingAddress.FirstName,
            LastName: command.ShippingAddress.LastName,
            EmailAddress: command.ShippingAddress.EmailAddress,
            Country: command.ShippingAddress.Country,
            State: command.ShippingAddress.State,
            Street: command.ShippingAddress.Street
        ),
        Items: command.Items.Select(i => new OrderItem(
            BookId: i.BookId,
            Title: i.Title,
            SellerId: i.SellerId,
            Quantity: i.Quantity,
        Price: i.Price
                )).ToList(),
                TotalPrice: command.Items.Sum(i => i.Quantity * i.Price)
            );

        await publishEndpoint.Publish(orderProcessedEvent);

        return mapper.Map<CreateOrderResult>(order);
    }
}
