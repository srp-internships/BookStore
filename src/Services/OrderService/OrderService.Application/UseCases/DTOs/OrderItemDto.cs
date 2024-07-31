namespace OrderService.Application.UseCases.DTOs;

public record OrderItemDto(Guid BookId, Guid SellerId, string Title, int Quantity, decimal Price);
