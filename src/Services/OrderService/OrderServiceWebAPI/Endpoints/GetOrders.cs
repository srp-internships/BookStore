using OrderService.Application.UseCases.DTOs;
using OrderService.Application.UseCases.Orders.Queries.GetOrders;

namespace OrderServiceWebAPI.Endpoints;

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PagingParameters pagingParameters, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(pagingParameters));

            return Results.Ok(result);
        })
        .WithName("GetOrders")
        .Produces<PaginatedResult<OrderDto>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}
