using AutoMapper;
using OrderService.Application.Orders.Commands.CreateOrder;
using OrderService.Application.Orders.Queries.GetOrders;

namespace OrderService.Application.Mappers;


public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
{
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderResult>();
        CreateMap<AddressDto, Address>();
        CreateMap<OrderItemDto, OrderItem>();
    }
}