namespace OrderService.Application.Dtos;


public record OrderItemDto(Guid OrderId, Guid BookId, int Quantity, decimal Price);
