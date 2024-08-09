using OrderService.Application.UseCases.Orders.Commands.DeleteOrder;

namespace OrderServiceWebAPI.Endpoints;

public class DeleteOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            var isSuccess = await sender.Send(new DeleteOrderCommand(id));

            return isSuccess ? Results.Ok(isSuccess) : Results.NotFound();
        })
        .WithName("DeleteOrder")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");
    }
}
