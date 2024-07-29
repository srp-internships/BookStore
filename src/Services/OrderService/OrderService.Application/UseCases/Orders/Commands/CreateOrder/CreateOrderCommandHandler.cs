using AutoMapper;
using OrderService.Application.Common.Interfaces.Repositories;


namespace OrderService.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IMapper mapper, IOrderRepository orderRepository)
    : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.CreateAsync(_mapper.Map<Order>(command), cancellationToken);
        return _mapper.Map<CreateOrderResult>(order);
    }
}
