using AutoMapper;
using OrderService.Application.Orders.Commands.CreateOrder;

namespace OrderService.Application.Mappers;


public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
{
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<Order, CreateOrderResult>();
        CreateMap<AddressDto, Address>();
        CreateMap<PaymentDto, Payment>();
        CreateMap<OrderItemDto, OrderItem>();
    }
}