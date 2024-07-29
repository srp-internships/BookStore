namespace OrderService.Application.Dtos;

public record OrderItemDto(Guid BookId, Guid SellerId, string Title, int Quantity, decimal Price);
