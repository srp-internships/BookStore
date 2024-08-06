using OrderService.IntegrationEvents;
using Address = OrderService.IntegrationEvents.Address;
using OrderStatus = OrderService.IntegrationEvents.OrderStatus;

namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IPublishEndpoint publishEndpoint, IMapper mapper, IOrderRepository orderRepository)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.CreateAsync(_mapper.Map<Order>(command), cancellationToken);

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
        Items: command.Items.Select(i => new IntegrationEvents.OrderItem(
            BookId: i.BookId,
            Title: i.Title,
            SellerId: i.SellerId,
            Quantity: i.Quantity,
        Price: i.Price
                )).ToList(),
                TotalPrice: command.Items.Sum(i => i.Quantity * i.Price)
            );

        await _publishEndpoint.Publish(orderProcessedEvent);

        return _mapper.Map<CreateOrderResult>(order);
    }


}
