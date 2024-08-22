using Microsoft.AspNetCore.Authorization;
using OrderService.Application.UseCases.DTOs;
using OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;
using System.Security.Claims;

namespace OrderServiceWebAPI.Endpoints;

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer", [Authorize(Roles = "customer")] async (HttpContext httpContext, ISender sender) =>
        {
            var orders = await sender.Send(new GetOrdersByCustomerQuery(Guid.Parse(httpContext.User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier).Value)));

            return Results.Ok(orders);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<IEnumerable<OrderDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By Customer")
        .RequireAuthorization();
    }
}
