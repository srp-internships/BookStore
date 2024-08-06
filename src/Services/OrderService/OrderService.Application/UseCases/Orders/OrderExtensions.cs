using OrderService.Application.UseCases.DTOs;

namespace OrderService.Application.UseCases.Orders
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => order.ToOrderDto());
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
                ShippingAddress: new AddressDto(
                    FirstName: order.ShippingAddress.FirstName,
                    LastName: order.ShippingAddress.LastName,
                    EmailAddress: order.ShippingAddress.EmailAddress ?? string.Empty,
                    Country: order.ShippingAddress.Country,
                    State: order.ShippingAddress.State,
                    Street: order.ShippingAddress.Street),
                Status: order.Status,
                TotalPrice: order.TotalPrice,
                Items: order.Items.Select(oi => new OrderItemDto(
                    BookId: oi.BookId,
                    SellerId: oi.SellerId,
                    Title: oi.Title,
                    Quantity: oi.Quantity,
                    Price: oi.Price)).ToList()
            );
        }
    }
}
