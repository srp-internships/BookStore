using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => new OrderDto(
            CustomerId: order.CustomerId,
            CartId: order.CartId,
            ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.Street),
            Status: order.Status,
            TotalPrice: order.TotalPrice,
            Items: order.Items.Select(oi => new OrderItemDto(oi.BookId, oi.SellerId, oi.Title, oi.Quantity, oi.Price)).ToList()
        ));
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        return DtoFromOrder(order);
    }

    private static OrderDto DtoFromOrder(Order order)
    {
        return new OrderDto(

                    CustomerId: order.CustomerId,
                    CartId: order.CartId,
                    ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.Street),
                    Status: order.Status,
                    TotalPrice: order.TotalPrice,
                    Items: order.Items.Select(oi => new OrderItemDto(oi.BookId, oi.SellerId, oi.Title, oi.Quantity, oi.Price)).ToList()
                );
    }
}

