using OrderService.Application.UseCases.DTOs;
using OrderService.Application.UseCases.Orders.Commands.CreateOrder;


namespace OrderService.Application.Mappers;

public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<CreateOrderCommand, Order>();
        CreateMap<AddressDto, Address>();
        CreateMap<OrderItemDto, OrderItem>();
    }
}