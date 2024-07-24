namespace OrderService.Application.Dtos;

public record OrderItemDto(Guid BookId, int Quantity, decimal Price);
