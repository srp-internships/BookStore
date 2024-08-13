using OrderService.Application.UseCases.DTOs;
using OrderService.Application.UseCases.Orders.Queries.GetOrdersByCustomer;

namespace OrderServiceWebAPI.Endpoints;

public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var orders = await sender.Send(new GetOrdersByCustomerQuery(customerId));

            return Results.Ok(orders);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<IEnumerable<OrderDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders By Customer")
        .WithDescription("Get Orders By Customer");
    }
}
