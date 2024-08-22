using Microsoft.AspNetCore.Authorization;

namespace OrderServiceWebAPI.Endpoints;

public class CreateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", [Authorize(Roles = "customer")] async (CreateOrderCommand command, ISender sender) =>
        {
            var orderId = await sender.Send(command);

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
