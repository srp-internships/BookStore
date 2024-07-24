using AutoMapper;
using OrderService.Application.Orders.Commands.CreateOrder;

namespace OrderService.Application.Mappers;


public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
{
        CreateMap<CreateOrderCommand, Order>()
            .ForMember(o => o.Items, opt => opt.MapFrom(command => command.Items));
        CreateMap<Order, CreateOrderResult>();
        CreateMap<AddressDto, Address>();
        CreateMap<OrderItemDto, OrderItem>();
    }
}