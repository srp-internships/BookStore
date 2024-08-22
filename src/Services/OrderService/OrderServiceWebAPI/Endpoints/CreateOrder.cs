using Microsoft.AspNetCore.Authorization;
using OrderService.Application.UseCases.DTOs;
using System.Security.Claims;

namespace OrderServiceWebAPI.Endpoints;

public class CreateOrder : ICarterModule
{
    public record CreateOrderRequest(
        Guid? CartId,
        AddressDto ShippingAddress,
        List<OrderItemDto> Items);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", [Authorize(Roles = "customer")] async (CreateOrderRequest command, ISender sender, HttpContext httpContext) =>
        {
            var orderId = await sender.Send(new CreateOrderCommand(
                Guid.Parse(httpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value),
                command.CartId,
                command.ShippingAddress,
                command.Items));

            return Results.Created($"/orders/{orderId}", new { Id = orderId });
        })
        .WithName("CreateOrder")
        .Produces(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Order")
        .WithDescription("Create Order")
        .RequireAuthorization();
    }
}
